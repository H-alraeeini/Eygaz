using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace Eygaz
{
    public static class DatabaseMigrator
    {
        private const string MigrationTableName = "SchemaMigrations";

        public static void EnsureMigrated(string dbPath)
        {
            if (string.IsNullOrWhiteSpace(dbPath))
                throw new ArgumentException("Database path is required.", nameof(dbPath));

            string dbDirectory = Path.GetDirectoryName(dbPath);
            if (!string.IsNullOrWhiteSpace(dbDirectory) && !Directory.Exists(dbDirectory))
                Directory.CreateDirectory(dbDirectory);

            string connectionString = "Data Source=" + dbPath;
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                EnableForeignKeys(connection);
                EnsureMigrationTable(connection);
                ApplyPendingMigrations(connection);
            }
        }

        private static void EnableForeignKeys(SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand("PRAGMA foreign_keys = ON;", connection))
            {
                command.ExecuteNonQuery();
            }
        }

        private static void EnsureMigrationTable(SQLiteConnection connection)
        {
            string sql = @"
CREATE TABLE IF NOT EXISTS SchemaMigrations (
    Version TEXT PRIMARY KEY,
    AppliedAt TEXT NOT NULL DEFAULT (datetime('now','localtime'))
);";

            using (var command = new SQLiteCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        private static void ApplyPendingMigrations(SQLiteConnection connection)
        {
            string migrationsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database", "Migrations");
            if (!Directory.Exists(migrationsDirectory))
                return;

            var appliedVersions = GetAppliedVersions(connection);
            var migrationFiles = Directory
                .GetFiles(migrationsDirectory, "*.sql")
                .OrderBy(Path.GetFileName, StringComparer.OrdinalIgnoreCase)
                .ToList();

            foreach (string migrationFile in migrationFiles)
            {
                string version = Path.GetFileName(migrationFile);
                if (appliedVersions.Contains(version))
                    continue;

                string sqlScript = ReadSqlScript(migrationFile);
                ApplySingleMigration(connection, version, sqlScript);
            }
        }

        private static string ReadSqlScript(string migrationFile)
        {
            // 1) Try BOM-aware reader first.
            using (var reader = new StreamReader(migrationFile, Encoding.UTF8, true))
            {
                string content = reader.ReadToEnd();
                if (!LooksLikeBrokenUnicode(content))
                    return content;
            }

            // 2) Fallback for UTF-16 files without BOM (common on Windows).
            byte[] bytes = File.ReadAllBytes(migrationFile);
            if (LooksLikeUtf16Le(bytes))
                return Encoding.Unicode.GetString(bytes);

            if (LooksLikeUtf16Be(bytes))
                return Encoding.BigEndianUnicode.GetString(bytes);

            // 3) Final fallback.
            return Encoding.UTF8.GetString(bytes);
        }

        private static bool LooksLikeBrokenUnicode(string content)
        {
            if (string.IsNullOrEmpty(content))
                return false;

            int nullCount = content.Count(ch => ch == '\0');
            // If text contains many nulls, it was decoded with the wrong encoding.
            return nullCount > content.Length / 10;
        }

        private static bool LooksLikeUtf16Le(byte[] bytes)
        {
            if (bytes == null || bytes.Length < 4 || (bytes.Length % 2) != 0)
                return false;

            int oddNulls = 0;
            int oddTotal = 0;
            for (int i = 1; i < bytes.Length; i += 2)
            {
                oddTotal++;
                if (bytes[i] == 0x00) oddNulls++;
            }

            return oddTotal > 0 && oddNulls >= (oddTotal * 7 / 10);
        }

        private static bool LooksLikeUtf16Be(byte[] bytes)
        {
            if (bytes == null || bytes.Length < 4 || (bytes.Length % 2) != 0)
                return false;

            int evenNulls = 0;
            int evenTotal = 0;
            for (int i = 0; i < bytes.Length; i += 2)
            {
                evenTotal++;
                if (bytes[i] == 0x00) evenNulls++;
            }

            return evenTotal > 0 && evenNulls >= (evenTotal * 7 / 10);
        }

        private static HashSet<string> GetAppliedVersions(SQLiteConnection connection)
        {
            var versions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            string sql = "SELECT Version FROM " + MigrationTableName + ";";
            using (var command = new SQLiteCommand(sql, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    versions.Add(reader["Version"].ToString());
                }
            }

            return versions;
        }

        private static void ApplySingleMigration(SQLiteConnection connection, string version, string sqlScript)
        {
            ExecuteMigrationStatements(connection, sqlScript);

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    using (var logCommand = new SQLiteCommand(
                        "INSERT INTO " + MigrationTableName + " (Version, AppliedAt) VALUES (@version, datetime('now','localtime'));",
                        connection,
                        transaction))
                    {
                        logCommand.Parameters.AddWithValue("@version", version);
                        logCommand.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Runs each SQL statement separately so a benign "already exists" failure does not roll back
        /// earlier statements in the same migration file.
        /// </summary>
        private static void ExecuteMigrationStatements(SQLiteConnection connection, string sqlScript)
        {
            foreach (string statement in SplitSqlStatements(sqlScript))
            {
                if (string.IsNullOrWhiteSpace(statement))
                    continue;

                try
                {
                    using (var command = new SQLiteCommand(statement, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (SQLiteException ex) when (IsIgnorableMigrationError(ex))
                {
                    // Table/column/index already present (e.g. manual change or re-run after partial apply).
                }
            }
        }

        /// <summary>
        /// Splits a script on ';' boundaries outside of single- and double-quoted literals.
        /// </summary>
        private static IEnumerable<string> SplitSqlStatements(string sqlScript)
        {
            if (string.IsNullOrEmpty(sqlScript))
                yield break;

            var builder = new StringBuilder(sqlScript.Length);
            bool inSingleQuote = false;
            bool inDoubleQuote = false;

            for (int i = 0; i < sqlScript.Length; i++)
            {
                char c = sqlScript[i];

                if (c == '\'' && !inDoubleQuote)
                {
                    inSingleQuote = !inSingleQuote;
                    builder.Append(c);
                    continue;
                }

                if (c == '\"' && !inSingleQuote)
                {
                    inDoubleQuote = !inDoubleQuote;
                    builder.Append(c);
                    continue;
                }

                if (c == ';' && !inSingleQuote && !inDoubleQuote)
                {
                    string part = builder.ToString().Trim();
                    if (part.Length > 0)
                        yield return part;

                    builder.Clear();
                    continue;
                }

                builder.Append(c);
            }

            string tail = builder.ToString().Trim();
            if (tail.Length > 0)
                yield return tail;
        }

        /// <summary>
        /// SQLite English messages for duplicate schema objects.
        /// </summary>
        private static bool IsIgnorableMigrationError(SQLiteException ex)
        {
            if (ex == null)
                return false;

            string msg = ex.Message;
            if (string.IsNullOrEmpty(msg))
                return false;

            if (msg.IndexOf("already exists", StringComparison.OrdinalIgnoreCase) >= 0)
                return true;

            if (msg.IndexOf("duplicate column name", StringComparison.OrdinalIgnoreCase) >= 0)
                return true;

            if (msg.IndexOf("duplicate column", StringComparison.OrdinalIgnoreCase) >= 0)
                return true;

            return false;
        }
    }
}

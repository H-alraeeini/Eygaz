using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Eygaz
{
    public class AuthService
    {
        private readonly Func f = new Func();
        private readonly string rememberFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Eygaz", "remember.me");

        public bool Login(string usernameOrEmail, string password, bool rememberMe, out string errorMessage)
        {
            errorMessage = "";
            if (string.IsNullOrWhiteSpace(usernameOrEmail) || string.IsNullOrWhiteSpace(password))
            {
                errorMessage = "يرجى إدخال اسم المستخدم وكلمة المرور.";
                return false;
            }

            string sql = @"
                SELECT u.Id, u.Username, u.DisplayName, u.MustChangePassword, r.RoleName
                FROM Users u
                INNER JOIN Roles r ON r.Id = u.RoleId
                WHERE (u.Username = @u OR u.Email = @u)
                  AND u.PasswordHash = @p
                  AND u.IsActive = 1;";

            var parameters = new Dictionary<string, object>
            {
                { "@u", usernameOrEmail.Trim() },
                { "@p", ComputeSha256(password.Trim()) }
            };

            DataTable dt = f.GetData(sql, parameters);
            if (dt == null || dt.Rows.Count == 0)
            {
                errorMessage = "اسم المستخدم أو كلمة المرور غير صحيحة.";
                return false;
            }

            DataRow row = dt.Rows[0];
            int userId = Convert.ToInt32(row["Id"]);
            string roleName = row["RoleName"].ToString();
            var permissions = GetUserPermissions(userId);

            AuthSession.SetSession(
                userId,
                row["Username"].ToString(),
                row["DisplayName"].ToString(),
                roleName,
                Convert.ToInt32(row["MustChangePassword"]) == 1,
                permissions);

            SaveRememberMe(rememberMe ? usernameOrEmail.Trim() : "");
            return true;
        }

        public bool ChangePassword(int userId, string newPassword)
        {
            if (userId <= 0 || string.IsNullOrWhiteSpace(newPassword)) return false;
            if (newPassword.Length < 6) return false;

            string sql = "UPDATE Users SET PasswordHash = @p, MustChangePassword = 0 WHERE Id = @id;";
            var parameters = new Dictionary<string, object>
            {
                { "@id", userId },
                { "@p", ComputeSha256(newPassword.Trim()) }
            };
            return f.ExecuteNonQuery(sql, parameters) > 0;
        }

        public bool CreateUser(string username, string displayName, string email, string password, int roleId, out string errorMessage)
        {
            errorMessage = "";
            if (!AuthSession.HasPermission("users.create"))
            {
                errorMessage = "ليس لديك صلاحية إنشاء المستخدمين.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                errorMessage = "اسم المستخدم وكلمة المرور مطلوبان.";
                return false;
            }

            string chkSql = "SELECT COUNT(*) FROM Users WHERE Username = @u OR Email = @e;";
            string exists = f.GetScalar(chkSql, new Dictionary<string, object> { { "@u", username.Trim() }, { "@e", email.Trim() } });
            if (!string.IsNullOrEmpty(exists) && exists != "0")
            {
                errorMessage = "اسم المستخدم أو البريد مستخدم مسبقاً.";
                return false;
            }

            string sql = @"
                INSERT INTO Users (Username, DisplayName, Email, PasswordHash, RoleId, IsActive, MustChangePassword, CreatedAt)
                VALUES (@u, @d, @e, @p, @r, 1, 0, datetime('now','localtime'));";

            var parameters = new Dictionary<string, object>
            {
                { "@u", username.Trim() },
                { "@d", displayName?.Trim() ?? "" },
                { "@e", email?.Trim() ?? "" },
                { "@p", ComputeSha256(password.Trim()) },
                { "@r", roleId }
            };

            return f.ExecuteNonQuery(sql, parameters) > 0;
        }

        public DataTable GetRoles()
        {
            return f.GetData("SELECT Id, RoleName FROM Roles ORDER BY RoleName;");
        }

        public DataTable GetAllUsers()
        {
            string sql = @"
                SELECT u.Id, u.Username, u.DisplayName, u.Email, r.RoleName, u.IsActive, u.MustChangePassword
                FROM Users u
                INNER JOIN Roles r ON r.Id = u.RoleId
                ORDER BY u.Username;";
            return f.GetData(sql);
        }

        public DataTable GetAllPermissions()
        {
            return f.GetData("SELECT Id, PermissionCode, PermissionName FROM Permissions ORDER BY PermissionCode;");
        }

        public DataTable GetRolePermissions(int roleId)
        {
            string sql = @"
                SELECT p.Id, p.PermissionCode, p.PermissionName,
                       CASE WHEN rp.RoleId IS NULL THEN 0 ELSE 1 END AS IsGranted
                FROM Permissions p
                LEFT JOIN RolePermissions rp ON rp.PermissionId = p.Id AND rp.RoleId = @roleId
                ORDER BY p.PermissionCode;";
            return f.GetData(sql, new Dictionary<string, object> { { "@roleId", roleId } });
        }

        public bool SetRolePermission(int roleId, int permissionId, bool granted)
        {
            if (!AuthSession.HasPermission("permissions.manage"))
                return false;

            if (granted)
            {
                string sql = "INSERT OR IGNORE INTO RolePermissions (RoleId, PermissionId) VALUES (@r, @p);";
                return f.ExecuteNonQuery(sql, new Dictionary<string, object> { { "@r", roleId }, { "@p", permissionId } }) >= 0;
            }
            else
            {
                string sql = "DELETE FROM RolePermissions WHERE RoleId = @r AND PermissionId = @p;";
                return f.ExecuteNonQuery(sql, new Dictionary<string, object> { { "@r", roleId }, { "@p", permissionId } }) >= 0;
            }
        }

        public bool CreateRole(string roleName)
        {
            if (!AuthSession.HasPermission("permissions.manage")) return false;
            if (string.IsNullOrWhiteSpace(roleName)) return false;
            string sql = "INSERT INTO Roles (RoleName, IsSystemRole) VALUES (@name, 0);";
            return f.ExecuteNonQuery(sql, new Dictionary<string, object> { { "@name", roleName.Trim() } }) > 0;
        }

        public DataTable GetUsersBasic()
        {
            return f.GetData("SELECT Id, Username FROM Users ORDER BY Username;");
        }

        public DataTable GetUserPermissionOverrides(int userId)
        {
            string sql = @"
                SELECT p.Id, p.PermissionCode, p.PermissionName,
                       CASE WHEN up.UserId IS NULL THEN 0 ELSE up.IsGranted END AS IsGranted
                FROM Permissions p
                LEFT JOIN UserPermissions up ON up.PermissionId = p.Id AND up.UserId = @uid
                ORDER BY p.PermissionCode;";
            return f.GetData(sql, new Dictionary<string, object> { { "@uid", userId } });
        }

        public bool SetUserPermissionOverride(int userId, int permissionId, bool granted)
        {
            if (!AuthSession.HasPermission("permissions.manage"))
                return false;

            string sql = @"
                INSERT INTO UserPermissions (UserId, PermissionId, IsGranted)
                VALUES (@u, @p, @g)
                ON CONFLICT(UserId, PermissionId)
                DO UPDATE SET IsGranted = excluded.IsGranted;";

            return f.ExecuteNonQuery(sql, new Dictionary<string, object>
            {
                { "@u", userId },
                { "@p", permissionId },
                { "@g", granted ? 1 : 0 }
            }) >= 0;
        }

        public string GetRememberedUsername()
        {
            try
            {
                if (!File.Exists(rememberFile)) return "";
                return File.ReadAllText(rememberFile, Encoding.UTF8).Trim();
            }
            catch
            {
                return "";
            }
        }

        private void SaveRememberMe(string username)
        {
            try
            {
                string dir = Path.GetDirectoryName(rememberFile);
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                File.WriteAllText(rememberFile, username ?? "", Encoding.UTF8);
            }
            catch { }
        }

        private List<string> GetUserPermissions(int userId)
        {
            var list = new List<string>();
            string sql = @"
                SELECT DISTINCT p.PermissionCode
                FROM Users u
                INNER JOIN RolePermissions rp ON rp.RoleId = u.RoleId
                INNER JOIN Permissions p ON p.Id = rp.PermissionId
                WHERE u.Id = @uid
                UNION
                SELECT DISTINCT p.PermissionCode
                FROM UserPermissions up
                INNER JOIN Permissions p ON p.Id = up.PermissionId
                WHERE up.UserId = @uid AND up.IsGranted = 1;";

            DataTable dt = f.GetData(sql, new Dictionary<string, object> { { "@uid", userId } });
            if (dt == null) return list;
            foreach (DataRow row in dt.Rows)
                list.Add(row["PermissionCode"].ToString());
            return list;
        }

        public static string ComputeSha256(string value)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(value ?? ""));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}

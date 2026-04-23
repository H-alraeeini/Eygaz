using System;
using System.Collections.Generic;

namespace Eygaz
{
    public static class AuthSession
    {
        public static int UserId { get; private set; }
        public static string Username { get; private set; }
        public static string DisplayName { get; private set; }
        public static string RoleName { get; private set; }
        public static bool MustChangePassword { get; private set; }

        private static HashSet<string> _permissions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        public static bool IsAuthenticated => UserId > 0;

        public static void SetSession(int userId, string username, string displayName, string roleName, bool mustChangePassword, IEnumerable<string> permissions)
        {
            UserId = userId;
            Username = username ?? "";
            DisplayName = displayName ?? "";
            RoleName = roleName ?? "";
            MustChangePassword = mustChangePassword;
            _permissions = new HashSet<string>(permissions ?? new string[0], StringComparer.OrdinalIgnoreCase);
        }

        public static bool HasPermission(string permissionCode)
        {
            if (!IsAuthenticated) return false;
            if (string.Equals(RoleName, "SuperAdmin", StringComparison.OrdinalIgnoreCase)) return true;
            return _permissions.Contains(permissionCode ?? "");
        }

        public static void Clear()
        {
            UserId = 0;
            Username = "";
            DisplayName = "";
            RoleName = "";
            MustChangePassword = false;
            _permissions.Clear();
        }

        public static void MarkPasswordChanged()
        {
            MustChangePassword = false;
        }
    }
}

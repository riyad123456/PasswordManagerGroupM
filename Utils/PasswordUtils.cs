using System.Text;
using PasswordManager.Models; 

namespace PasswordManager.Utils;
internal static class PasswordUtils
{
    public static string DecryptPassword(string encryptedPassword)
    {
        var data = Convert.FromBase64String(encryptedPassword);
        return Encoding.UTF8.GetString(data);
    }

    public static string EncryptPassword(string plainPassword)
    {
        var data = Encoding.UTF8.GetBytes(plainPassword);
        return Convert.ToBase64String(data);
    }

    public static string? ValidateEntry(PasswordEntry? entry)
    {
        if (entry == null)
            return "Entry cannot be null.";

        if (string.IsNullOrWhiteSpace(entry.Category))
            return "Category cannot be null or empty.";

        if (string.IsNullOrWhiteSpace(entry.App))
            return "App cannot be null or empty.";

        if (string.IsNullOrWhiteSpace(entry.UserName))
            return "UserName cannot be null or empty.";

        if (string.IsNullOrWhiteSpace(entry.Password))
            return "EncryptedPassword cannot be null or empty.";

        return null;
    }
}
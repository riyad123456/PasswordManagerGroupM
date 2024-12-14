using PasswordManager.Utils;

namespace PasswordManager.Models;
public record PasswordEntry(int Id, string Category, string App, string UserName, string Password)
{
    public int Id { get; set; } = Id;
    public string Category { get; set; } = Category;
    public string App { get; set; } = App;
    public string UserName { get; set; } = UserName;
    public string Password { get; set; } = PasswordUtils.EncryptPassword(Password);
}
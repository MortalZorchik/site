namespace WebApplication1.Domain.ViewModels;

public class UserViewModel
{
    public string Name { get; set; } = ""; // имя пользователя

    public string Password { get; set; }

    public Role Role { get; set; } // возраст пользователя
}
namespace WebApplication1.Domain.Entity;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = ""; // имя пользователя

    public string Password { get; set; }

    public Role Role { get; set; } // возраст пользователя
}
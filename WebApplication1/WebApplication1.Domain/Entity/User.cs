using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Domain.Entity;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = ""; // имя пользователя

    [MinLength(6, ErrorMessage = "Минимальная длина должна быть >=6 символов")]
    public string Password { get; set; }

    public Role Role { get; set; } // возраст пользователя
}
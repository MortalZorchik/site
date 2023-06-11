using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Domain.ViewModels;

public class UserViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = ""; // имя пользователя
    public string Password { get; set; }
    [Display(Name = "Роль")]
    [Required(ErrorMessage = "Выберите роль")]
    public Role Role { get; set; } // возраст пользователя
}
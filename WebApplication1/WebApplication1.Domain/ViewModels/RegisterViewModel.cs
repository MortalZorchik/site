using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Domain.ViewModels;

public class RegisterViewModel
{
    
    [Required(ErrorMessage = "Укажите имя")]
    public string Name { get; set; }
    
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Укажите пароль")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Подтвердите пароль")]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    public string PasswordConfirm { get; set; }
}
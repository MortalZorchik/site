using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Domain.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Введите имя")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Введите пароль")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }
}
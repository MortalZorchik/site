using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Domain;

public enum Role
{
    [Display(Name = "Пользователь")]
    User = 0,
    [Display(Name = "Модератор")]
    Moderator = 1,
    [Display(Name = "Админ")]
    Admin = 2
}
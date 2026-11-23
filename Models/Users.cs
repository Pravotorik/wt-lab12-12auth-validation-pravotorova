using System.ComponentModel.DataAnnotations;

namespace wt_lab12_12auth_validation_pravotorova.Models;

public class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Введите логин")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Логин должен быть от 3 до 50 символов")]
    public string Login { get; set; } = "";

    [Required(ErrorMessage = "Введите пароль")]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Пароль должен быть не менее 4 символов")]
    public string Password { get; set; } = "";

    [StringLength(100)]
    public string FullName { get; set; } = "";

    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = "";

    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
}
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Stock.Models.VM
{
    [Keyless]
    public class RegistrationUser
    {
        [Required(ErrorMessage = "Требуется Фамилия")]
        [StringLength(50, ErrorMessage = "Фамилия не может быть более 50 символов")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Требуется Имя")]
        [StringLength(50, ErrorMessage = "Имя не может быть более 50 символов")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Требуется Email")]
        [StringLength(100, ErrorMessage = "Email не может быть более 100 символов")]
        [EmailAddress(ErrorMessage = "Пожалуйста, введите коректный Email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Требуется Логин")]
        [StringLength(100, ErrorMessage = "Логин не может быть длинее 100 символов")]
        [Display(Name = "Логин")]
        public string Login { get; set; }


        [Required(ErrorMessage = "Требуется Пароль")]
        [StringLength(20, ErrorMessage = "Пароль не может быть длинее 20 символов")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }


        [Compare("Password", ErrorMessage = "Пожалуйста, подтвердите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        public string ConfirmPassword { get; set; }
    }
}

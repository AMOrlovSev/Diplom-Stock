using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Stock.Models
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(Login), IsUnique = true)]
    public class User
    {
        [Key]
        public int UserID { get; set; }

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
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Требуется Логин")]
        [StringLength(100, ErrorMessage = "Логин не может быть более 100 символов")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Требуется Пароль")]
        [StringLength(20, ErrorMessage = "Пароль не может быть более 20 символов")]
        [DataType(DataType.Password)]
        public string Password { get; set; }



        public ICollection<Portfolio>? Portfolios { get; set; }
    }
}

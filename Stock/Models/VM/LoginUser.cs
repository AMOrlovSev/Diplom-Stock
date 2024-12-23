using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Stock.Models.VM
{
    [Keyless]
    public class LoginUser
    {
        [Required(ErrorMessage = "Требуется Логин или Email")]
        [StringLength(100, ErrorMessage = "Логин или Email не могут быть более 100 символов")]
        [DisplayName("Логин или Email")]
        public string LoginOrEmail { get; set; }

        [Required(ErrorMessage = "Требуется пароль")]
        [StringLength(20, ErrorMessage = "Пароль не может быть более 20 символов")]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public string Password { get; set; }
    }
}

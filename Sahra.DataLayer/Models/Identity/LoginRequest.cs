using System.ComponentModel.DataAnnotations;

namespace Sahra.DataLayer.Models.Identity
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "نام کاربری اجباری می باشد.")]

        public string Username { get; set; }

        [Required(ErrorMessage = "نام کاربری اجباری می باشد.")]
        public string Password { get; set; }
    }
}

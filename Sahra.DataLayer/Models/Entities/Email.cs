using System.ComponentModel.DataAnnotations;

namespace Sahra.DataLayer.Models.Entities
{
    public class Email : BaseEntity
    {
        [EmailAddress(ErrorMessage = " خطا در درج ایمیل")]
        public string Mail { get; set; }
        public string Description { get; set; }
    }
}

using DNTPersianUtils.Core;
using System.ComponentModel.DataAnnotations;

namespace Sahra.DataLayer.Models.Entities
{
    public class Members : RelationOfInvestReq
    {
        [Required(ErrorMessage = "درج نام اجباری است")]
        public string Name { get; set; }
        [Required(ErrorMessage = "درج سمت اجباری است")]
        public string Position { get; set; }
        [Required(ErrorMessage = "درج ایمیل اجباری است")]
        [EmailAddress(ErrorMessage = "خطا در درج ایمیل")]
        public string Email { get; set; }
        [Required(ErrorMessage = "درج تحصیلات اجباری است")]
        public string DegreeOfEducation { get; set; }
        [Required(ErrorMessage = "درج شماره مبایل اجباری است")]
        [ValidIranianPhoneNumber(ErrorMessage = "خطا در درج شماره همراه")]
        public string Mobile { get; set; }
    }
}

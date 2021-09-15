using DNTPersianUtils.Core;
using Sahra.DataLayer.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sahra.DataLayer.Models.Entities
{

    public class InvestRequest : BaseEntity
    {
        [Required(ErrorMessage = " خطا در درج نام")]
        public string Name { get; set; }
        [Required(ErrorMessage = "خطا در درج نماینده")]
        public string AgentName { get; set; }
        [Required(ErrorMessage = "درج ایمیل اجباری است")]
        [EmailAddress(ErrorMessage = "خطا در درج ایمیل")]
        public string StartUpEmail { get; set; }
        [Required(ErrorMessage = "درج شماره مبایل اجباری است")]
        [ValidIranianPhoneNumber(ErrorMessage = "خطا در درج شماره همراه")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "درج نفرات اجباری است")]
        public int TeamPersonCount { get; set; }
        [Required(ErrorMessage = "درج ثبت نام کرده اید یا نه اجباری است")]
        public bool IsRegister { get; set; }
        [Required(ErrorMessage = "درج توضیحات اجباری است")]
        public string Description { get; set; }
        [Required(ErrorMessage = "درج چشم انداز اجباری است")]
        public string Vision { get; set; }
        [Required]
        public int TrackingNumber { get; set; }
        public string WebSiteAddress { get; set; }
        [Required(ErrorMessage = "درج آیا محصول دارید اجباری است")]
        public bool HasProductIdea { get; set; }
        [Required(ErrorMessage = "درج آیا کاربر فعال داربد اجباری است")]
        public bool IsActiveUser { get; set; }
        [Required(ErrorMessage = "درج نیاز شما اجباری است")]
        public string Need { get; set; }
        [Required(ErrorMessage = "درج سرمایه اجباری است")]
        public string Invest { get; set; }

        public ICollection<Platform> Platform { get; set; }
        [Required]
        public ICollection<Members> Members { get; set; }
        [Required]
        public ICollection<Collabration> Collabration { get; set; }
        [Required]
        public ICollection<CategoryInvestReq> Category { get; set; }

        public ICollection<UploadFile> UploadFile { get; set; }

        public EnumRequestStatus RequestStatus { get; set; }



    }
}

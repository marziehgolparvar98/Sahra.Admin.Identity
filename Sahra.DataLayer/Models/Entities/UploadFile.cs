using System.ComponentModel.DataAnnotations;

namespace Sahra.DataLayer.Models.Entities
{
    public class UploadFile : RelationOfInvestReq
    {
        [Required(ErrorMessage = "درج فایل اجباری است")]
        public string UploadFileName { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace Sahra.DataLayer.Models.Entities
{
    public class Category : BaseEntity
    {
        [Required(ErrorMessage = "خطا در درج عنوان")]
        public string Title { get; set; }
        public int? Parent { get; set; }
        public bool IsDeleted { get; set; }

    }
}

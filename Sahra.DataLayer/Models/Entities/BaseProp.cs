using System.ComponentModel.DataAnnotations;

namespace Sahra.DataLayer.Models.Entities
{
    public class BaseProp : BaseEntity
    {
        [Required(ErrorMessage = "درج عنوان اجباری میباشد")]
        public string Title { get; set; }
        [Required(ErrorMessage = " درج تصویر اجباری میباشد ")]
        public string Image { get; set; }
    }
}

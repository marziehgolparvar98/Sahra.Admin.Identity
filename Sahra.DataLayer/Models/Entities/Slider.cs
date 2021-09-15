using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sahra.DataLayer.Models.Entities
{
    public class Slider : BaseEntity
    {
        [Required(ErrorMessage = "درج توضیحات اسلاید اجباری است")]
        public string SliderDescription { get; set; }
        public ICollection<SliderImage> SliderImage { get; set; }
    }
}

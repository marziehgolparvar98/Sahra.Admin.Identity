using System.ComponentModel.DataAnnotations;

namespace Sahra.DataLayer.Models.Entities
{
    public class SliderImage : BaseProp
    {
        [Required(ErrorMessage = "درج SlideAlt اجباری است")]
        public string SlideAlt { get; set; }

        public int SliderId { get; set; }

        public Slider Slider { get; set; }

    }
}

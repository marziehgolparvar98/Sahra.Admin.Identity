using Microsoft.AspNetCore.Http;

namespace Sahra.ViewModel.SliderImage
{
    public class AddImageSliderViewModel
    {
        public string SlideTitle { get; set; }
        public string SlideAlt { get; set; }
        public IFormFile SlideImage { get; set; }
        public int SliderId { get; set; }
    }
}

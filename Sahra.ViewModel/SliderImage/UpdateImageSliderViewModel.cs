using Microsoft.AspNetCore.Http;

namespace Sahra.ViewModel.SliderImage
{
    public class UpdateImageSliderViewModel
    {
        public int SlideId { get; set; }
        public string SliderTitle { get; set; }
        public string Alt { get; set; }
        public IFormFile Image { get; set; }
    }
}

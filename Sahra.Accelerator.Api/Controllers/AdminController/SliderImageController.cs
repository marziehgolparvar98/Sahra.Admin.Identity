using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sahara.Common;
using Sahra.DataLayer.Models.Identity;
using Sahra.Services.IService;
using Sahra.ViewModel.SliderImage;
using System.Threading.Tasks;

namespace Accelerator.Api.Controllers.AdminController
{
    [Authorize(Roles = UserRole.Admin)]
    [ApiController]
    [Route("api/admin/[controller]")]
    [ApiExplorerSettings(GroupName = "AdminV1")]
    public class SliderImageController : ControllerBase
    {
        private readonly ISliderImageService _sliderImageService;
        public SliderImageController(ISliderImageService sliderImageService)
        {
            _sliderImageService = sliderImageService;
        }

        [HttpDelete("DeleteSlidereImg")]
        public async Task<IActionResult> DeleteSlidereImg([FromQuery] int id)
        {
            var result =
            await _sliderImageService.DeleteSliderImage(id);
            return result.ToHttpCodeResult();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSlider([FromForm] UpdateImageSliderViewModel updateImageSliderViewModel)
        {
            var result =
                    await _sliderImageService.UpdateSlide(updateImageSliderViewModel);
            return result.ToHttpCodeResult();
        }

        [HttpPost("AddImageToSlider")]
        public async Task<IActionResult> AddImageToSlider([FromForm] AddImageSliderViewModel addImageSliderViewModel)
        {
            var result =
                await _sliderImageService.AddImageToSlider(addImageSliderViewModel);
            return result.ToHttpCodeResult();
        }
    }
}

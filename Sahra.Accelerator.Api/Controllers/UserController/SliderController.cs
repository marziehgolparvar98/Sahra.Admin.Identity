using Microsoft.AspNetCore.Mvc;
using Sahara.Common;
using Sahra.Services.IService;
using System;
using System.Threading.Tasks;

namespace Sahra.Accelerator.api.Controllers.UserController
{
    [Route("api/[controller]")]
    [ApiController]

    public class SliderController : ControllerBase
    {
        private readonly ISliderService _sliderService;
        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSlider()
        {
            var result =
                 await _sliderService.GetAllSlider();
            return result.ToHttpCodeResult();
        }

        [HttpGet("GetSliderById")]
        public async Task<IActionResult> GetSliderById(int id)
        {
            var result =
            await _sliderService.GetSliderById(id);
            return result.ToHttpCodeResult();
        }

        [HttpGet("images/{imagename}")]
        public IActionResult GetSlider(string imagename)
        {
            try
            {
                var imagePathResult = UploadFileExtension.ReadFile("Slider", imagename);
                if (imagePathResult == null)
                    return NotFound();
                return File(imagePathResult, UploadFileExtension.GetMimeType(imagename));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}

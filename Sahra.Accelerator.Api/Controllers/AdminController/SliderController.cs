using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sahara.Common;
using Sahra.DataLayer.Models.Identity;
using Sahra.Services.IService;
using Sahra.ViewModel.Slider;
using System.Threading.Tasks;

namespace Sahra.Accelerator.api.Controllers.AdminController
{
    [Authorize(Roles = UserRole.Admin)]
    [ApiController]
    [Route("api/admin/[controller]")]
    [ApiExplorerSettings(GroupName = "AdminV1")]
    public class SliderController : ControllerBase
    {
        private readonly ISliderService _sliderService;
        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSlider(int id)
        {
            var result =
                 await _sliderService.DeleteSlider(id);
            return result.ToHttpCodeResult();
        }

        [HttpPost]
        public async Task<IActionResult> AddSlider([FromForm] AddSliderViewModel addSliderViewModel)
        {
            var response = await _sliderService.AddSlider(addSliderViewModel);
            return response.ToHttpCodeResult();
        }
    }
}

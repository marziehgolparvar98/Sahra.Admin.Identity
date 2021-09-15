using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sahara.Common;
using Sahra.DataLayer.Models.Identity;
using Sahra.Services.IService;
using Sahra.ViewModel.Event;
using System.Threading.Tasks;

namespace Sahra.Accelerator.api.Controllers.AdminController
{
    [Authorize(Roles = UserRole.Admin)]
    [ApiController]
    [Route("api/admin/[controller]")]
    [ApiExplorerSettings(GroupName = "AdminV1")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpDelete]
        public async Task<IActionResult> AddEvent(int id)
        {
            var result =
                  await _eventService.DeleteEvent(id);
            return result.ToHttpCodeResult();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNews([FromForm] UpdateEventViewModel updateEventViewModel)
        {
            var result =
                    await _eventService.UpdateEvent(updateEventViewModel);
            return result.ToHttpCodeResult();
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent([FromForm] AddEventViewModel addEventViewModel)
        {
            var response = await _eventService.AddEvent(addEventViewModel);
            return response.ToHttpCodeResult();
        }
    }
}
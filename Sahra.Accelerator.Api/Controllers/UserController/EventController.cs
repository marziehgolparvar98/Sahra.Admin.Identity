using Microsoft.AspNetCore.Mvc;
using Sahara.Common;
using Sahra.Services.IService;
using System;
using System.Threading.Tasks;

namespace Sahra.Accelerator.api.Controllers.UserController
{
    [Route("api/[controller]")]
    [ApiController]

    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEvent(int page, int pageSize)
        {
            var result =
                     await _eventService.GetAllEvent(page, pageSize);
            return result.ToHttpCodeResult();
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetEventById(int Id)
        {
            var result =
            await _eventService.GetEventById(Id);
            return result.ToHttpCodeResult();
        }

        [HttpGet("images/{imagename}")]
        public IActionResult GetImegs(string imagename)
        {
            try
            {
                var imagePathResult = UploadFileExtension.ReadFile("Event", imagename);
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

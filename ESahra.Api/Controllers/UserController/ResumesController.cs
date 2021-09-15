using Microsoft.AspNetCore.Mvc;
using Sahara.Common;
using Sahra.Services.IService;
using Sahra.ViewModel.Resume;
using System.Threading.Tasks;

namespace ESahra.Api.Controllers.UserController
{

    [Route("api/[controller]")]
    [ApiController]
    public class ResumesController : ControllerBase
    {
        private readonly IResumeService _resumeService;
        public ResumesController(IResumeService resumeService)
        {
            _resumeService = resumeService;
        }

        [HttpPost]
        public async Task<IActionResult> AddResume([FromForm] AddResume addResume)
        {
            var response =
               await _resumeService.AddResume(addResume);
            return response.ToHttpCodeResult();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateResume([FromForm] UpdateResumeViewModel update)
        {
            var response =
               await _resumeService.UpdateResume(update);
            return response.ToHttpCodeResult();
        }
    }
}

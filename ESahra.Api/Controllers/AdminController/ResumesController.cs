using Microsoft.AspNetCore.Mvc;
using Sahara.Common;
using Sahra.Services.IService;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;

namespace ESahra.Api.Controllers.AdminController
{
#if !DEBUG
[Authorize]
#endif
    [ApiController]
    [Route("api/admin/[controller]")]
    public class ResumesController : ControllerBase
    {
        private readonly IResumeService _resumeService;
        public ResumesController(IResumeService resumeService)
        {
            _resumeService = resumeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllResume()
        {
            var result =
                await _resumeService.GetAllResume();
            return result.ToHttpCodeResult();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdResume(int id)
        {
            var result =
                await _resumeService.GetResumeById(id);
            return result.ToHttpCodeResult();
        }
    }
}

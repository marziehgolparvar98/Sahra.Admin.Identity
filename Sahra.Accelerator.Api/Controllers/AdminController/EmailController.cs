using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sahara.Common;
using Sahra.DataLayer.Models.Identity;
using Sahra.Services.IService;
using System.Threading.Tasks;


namespace Sahra.Accelerator.api.Controllers.AdminController
{

    [Authorize(Roles = UserRole.Admin)]
    [ApiController]
    [ApiExplorerSettings(GroupName = "AdminV1")]
    [Route("api/admin/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmail(int page, int pageSize)
        {
            var result =
                await _emailService.GetAllEmail(page, pageSize);
            return result.ToHttpCodeResult();
        }
    }
}

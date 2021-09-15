using Microsoft.AspNetCore.Mvc;
using Sahara.Common;
using Sahra.Services.IService;
using Sahra.ViewModel.Mail;
using System.Threading.Tasks;

namespace Sahra.Accelerator.api.Controllers.UserController
{
    [Route("api/[controller]")]
    [ApiController]

    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> AddEmail(AddEmailViewModel addEmailViewModel)
        {
            var response =
               await _emailService.AddEmail(addEmailViewModel);
            return response.ToHttpCodeResult();
        }

    }
}

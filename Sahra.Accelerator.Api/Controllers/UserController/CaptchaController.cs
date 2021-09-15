using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sahra.Common;

namespace Sahra.Accelerator.api.Controllers.UserController
{
    [Route("api/[controller]")]
    [ApiController]

    public class CaptchaController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CaptchaResult))]
        public IActionResult Get()
        {
            string captchaCode = null;
            var captcha = Captcha.GenerateCaptchaImage(ref captchaCode);
            CaptchaRepository.AllCaptcha.TryAdd($"{captcha.HashedCaptcha}-{captcha.Salt}", false);
            return Ok(captcha);
        }
    }
}

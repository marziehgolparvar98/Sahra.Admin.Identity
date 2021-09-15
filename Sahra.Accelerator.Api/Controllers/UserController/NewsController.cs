using Microsoft.AspNetCore.Mvc;
using Sahara.Common;
using Sahra.Services.IService;
using System;
using System.Threading.Tasks;

namespace Sahra.Accelerator.api.Controllers.UserController
{
    [Route("api/[controller]")]
    [ApiController]

    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNews(int page, int pageSize)
        {
            var result =
                 await _newsService.GetAllNews(page, pageSize);
            return result.ToHttpCodeResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNewsById(int id)
        {
            var result =
            await _newsService.GetNewsById(id);
            return result.ToHttpCodeResult();
        }

        [HttpGet("images/{imagename}")]
        public IActionResult GetImegs(string imagename)
        {
            try
            {
                var imagePathResult = UploadFileExtension.ReadFile("News", imagename);
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

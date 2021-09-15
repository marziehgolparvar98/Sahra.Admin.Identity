using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sahara.Common;
using Sahra.DataLayer.Models.Identity;
using Sahra.Services.IService;
using Sahra.ViewModel.News;
using System.Threading.Tasks;

namespace Sahra.Accelerator.api.Controllers.AdminController
{
    [Authorize(Roles = UserRole.Admin)]
    [ApiController]
    [Route("api/admin/[controller]")]
    [ApiExplorerSettings(GroupName = "AdminV1")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            var result =
                await _newsService.DeleteNews(id);
            return result.ToHttpCodeResult();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNews([FromForm] UpdateNewsViewModel updateNewsViewModel)
        {
            var result =
                 await _newsService.UpdateNews(updateNewsViewModel);
            return result.ToHttpCodeResult();
        }
        [HttpPost]
        public async Task<IActionResult> AddNews([FromForm] AddNewsViewModel addNewsModel)
        {
            var response = await _newsService.AddNews(addNewsModel);
            return response.ToHttpCodeResult();
        }
    }
}


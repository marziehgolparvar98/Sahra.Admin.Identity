using Microsoft.AspNetCore.Mvc;
using Sahara.Common;
using Sahra.Services.IService;
using System.Threading.Tasks;

namespace Sahra.Accelerator.api.Controllers.UserController
{
    [Route("api/[controller]")]
    [ApiController]

    public class NewsCategoryController : ControllerBase
    {
        private readonly INewsCategoryService _newsCategoryService;
        public NewsCategoryController(INewsCategoryService newsCategoryService)
        {
            _newsCategoryService = newsCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var result =
               await _newsCategoryService.GetAllCategory();
            return result.ToHttpCodeResult();
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            var result =
              await _newsCategoryService.GetCategoryById(categoryId);
            return result.ToHttpCodeResult();
        }
    }
}


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sahara.Common;
using Sahra.DataLayer.Models.Identity;
using Sahra.Services.IService;
using Sahra.ViewModel.NewsCategory;
using System.Threading.Tasks;

namespace Accelerator.Api.Controllers.AdminController
{
    [Authorize(Roles = UserRole.Admin)]
    [ApiController]
    [Route("api/admin/[controller]")]
    [ApiExplorerSettings(GroupName = "AdminV1")]
    public class NewsCategoryController : ControllerBase
    {

        private readonly INewsCategoryService _newsCategoryService;
        public NewsCategoryController(INewsCategoryService newsCategoryService)
        {
            _newsCategoryService = newsCategoryService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromForm] AddCategoryViewModel addCategoryViewModel)
        {
            var result = await _newsCategoryService.AddCategory(addCategoryViewModel);
            return result.ToHttpCodeResult();

        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromForm] UpdateNewsCategoryViewModel updateNewsCategoryViewModel)
        {
            var result =
                await _newsCategoryService.UpdateCategory(updateNewsCategoryViewModel);
            return result.ToHttpCodeResult();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result =
                await _newsCategoryService.DeleteCategory(id);
            return result.ToHttpCodeResult();
        }

        [HttpGet("GetAllCategory")]
        public async Task<IActionResult> GetAllCategory()
        {
            var result =
               await _newsCategoryService.GetAllCategory();
            return result.ToHttpCodeResult();
        }
    }
}


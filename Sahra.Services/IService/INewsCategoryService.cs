using Sahara.Common;
using Sahra.DataLayer.Models.Entities;
using Sahra.ViewModel.NewsCategory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahra.Services.IService
{
    public interface INewsCategoryService
    {
        Task<Result<List<NewsCategory>>> GetAllCategory();
        Task<Result<NewsCategory>> AddCategory(AddCategoryViewModel addCategoryViewModel);
        Task<Result<ShowNewsCategoryViewModel>> UpdateCategory(UpdateNewsCategoryViewModel updateNewsCategoryViewModel);
        Task<Result<NewsCategory>> DeleteCategory(int id);
        Task<Result<List<NewsCategory>>> GetParent(int categoryId);
        Task<Result<NewsCategory>> GetCategoryById(int categoryId);
        Task<Result> ExistCategory(string title, int cateId);
        Task<Result> IsExist(int? cateId);
    }
}

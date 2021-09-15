using Sahara.Common;
using Sahra.DataLayer.Models.DTO;
using Sahra.DataLayer.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahra.DataLayer.IRepository
{
    public interface INewsCategoryRepository
    {
        Task<Result<List<NewsCategory>>> GetAllCategory();
        Task<Result<NewsCategory>> AddCategory(NewsCategory newsCategory);
        Task<Result<NewsCategory>> UpdateCategory(UpdateNewsCategoryDTO updateNewsCategoryDTO);
        Task<Result<NewsCategory>> DeleteCategory(int id);
        Task<Result<List<NewsCategory>>> GetParent(int categoryId);
        Task<Result<NewsCategory>> GetCategoryById(int categoryId);
        Task<Result> ExistCategory(string title, int cateId);
        Task<Result> IsEndOfChain(int cateId);
        Task<Result> IsExist(int? cateId);

    }
}

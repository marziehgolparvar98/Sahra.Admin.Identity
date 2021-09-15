using Microsoft.EntityFrameworkCore;
using Sahara.Common;
using Sahra.DataLayer.IRepository;
using Sahra.DataLayer.Models.DbContext;
using Sahra.DataLayer.Models.DTO;
using Sahra.DataLayer.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sahra.DataLayer.Repository
{
    public class NewsCategoryRepository : INewsCategoryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public NewsCategoryRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<Result<NewsCategory>> AddCategory(NewsCategory newsCategory)
        {
            _applicationDbContext.NewsCategories.Add(newsCategory);
            await _applicationDbContext.SaveChangesAsync();
            return Result.Success(newsCategory);
        }

        public async Task<Result<NewsCategory>> DeleteCategory(int id)
        {
            var cate = await _applicationDbContext.NewsCategories.FirstOrDefaultAsync(current => current.Id == id);
            if (cate != null)
            {
                _applicationDbContext.NewsCategories.Remove(cate);

                await _applicationDbContext.SaveChangesAsync();
                return Result.Success(cate);
            }
            return Result.Failed<NewsCategory>("کتگوری با این آیدی یافت نشد.");
        }

        public async Task<Result> ExistCategory(string title, int cateId)
        {
            var result = await _applicationDbContext.NewsCategories.AnyAsync(c => c.Title == title && c.Parent == cateId);
            if (result)
            {
                return Result.Success(result);
            }
            return Result.Failed(" ");
        }

        public async Task<Result<List<NewsCategory>>> GetAllCategory()
        {
            var result = await _applicationDbContext.NewsCategories.Where(c => !c.IsDeleted).ToListAsync();
            return Result.Success(result);
        }

        public async Task<Result<NewsCategory>> GetCategoryById(int categoryId)
        {
            var result = await _applicationDbContext.NewsCategories
            .Include(x => x.News)
            .FirstOrDefaultAsync(current => current.Id == categoryId);
            if (result == null)
            {
                return Result.Failed<NewsCategory>("کتگوری با این آیدی یافت نشد.");
            }
            return Result.Success(result);
        }

        public async Task<Result<List<NewsCategory>>> GetParent(int categoryId)
        {
            var result = await _applicationDbContext.NewsCategories.Where(c => !c.IsDeleted && c.Parent == categoryId).ToListAsync();
            return Result.Success(result);
        }

        public async Task<Result> IsEndOfChain(int cateId)
        {
            var result = await _applicationDbContext.NewsCategories.AnyAsync(c => c.Parent == cateId);
            if (result)
            {
                return Result.Failed("");
            }
            return Result.Success();
        }

        public async Task<Result> IsExist(int? cateId)
        {
            if (cateId == null)
                return Result.Success();

            var result = await _applicationDbContext.NewsCategories.FirstOrDefaultAsync(c => c.Id == cateId);
            if (result == null)
            {
                return Result.Failed("");

            }
            return Result.Success();
        }

        public async Task<Result<NewsCategory>> UpdateCategory(UpdateNewsCategoryDTO updateNewsCategoryDTO)
        {
            var upNew = await _applicationDbContext.NewsCategories.FirstOrDefaultAsync(current => current.Id == updateNewsCategoryDTO.Id);
            if (upNew != null)
            {
                upNew.Id = updateNewsCategoryDTO.Id;
                upNew.Title = updateNewsCategoryDTO.CategoryTitle;
                //upNew.parent = updateNewsCategoryDTO.parent;
                await _applicationDbContext.SaveChangesAsync();
                return Result.Success(upNew);
            }
            return Result.Failed<NewsCategory>("کاربری با این آیدی یافت نشد!!");
        }
    }
}
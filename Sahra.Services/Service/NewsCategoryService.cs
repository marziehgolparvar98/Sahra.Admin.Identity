using Microsoft.Extensions.Logging;
using Sahara.Common;
using Sahra.DataLayer.IRepository;
using Sahra.DataLayer.Models.DTO;
using Sahra.DataLayer.Models.Entities;
using Sahra.Services.IService;
using Sahra.ViewModel.NewsCategory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahra.Services.Service
{
    public class NewsCategoryService : INewsCategoryService
    {
        private readonly INewsCategoryRepository _newsCategoryRepository;
        private readonly ILogger<NewsCategoryService> _logger;
        public NewsCategoryService(INewsCategoryRepository newsCategoryRepository, ILogger<NewsCategoryService> logger)
        {
            _newsCategoryRepository = newsCategoryRepository;
            _logger = logger;
        }
        public async Task<Result<NewsCategory>> AddCategory(AddCategoryViewModel addCategoryViewModel)
        {

            var exi = await _newsCategoryRepository.ExistCategory(addCategoryViewModel.CategoryTitle, addCategoryViewModel.Parent ?? 0);

            var notfound = await _newsCategoryRepository.IsExist(addCategoryViewModel.Parent);
            if (notfound.NotSucceeded)
                return Result.Failed<NewsCategory>("این ساب کتگوری وجود ندارد");

            if (exi.Succeeded)
                return Result.Failed<NewsCategory>("تکراری است");


            var ac = new NewsCategory();
            ac.Title = addCategoryViewModel.CategoryTitle;
            ac.Parent = addCategoryViewModel.Parent;
            ac.CreateDate = DateTime.Now;

            return await _newsCategoryRepository.AddCategory(ac);
        }

        public async Task<Result<NewsCategory>> DeleteCategory(int id)
        {
            return await _newsCategoryRepository.DeleteCategory(id);
        }

        public async Task<Result> ExistCategory(string title, int cateId)
        {
            return await _newsCategoryRepository.ExistCategory(title, cateId);
        }

        public async Task<Result<List<NewsCategory>>> GetAllCategory()
        {
            return await _newsCategoryRepository.GetAllCategory();
        }

        public async Task<Result<NewsCategory>> GetCategoryById(int categoryId)
        {
            return await _newsCategoryRepository.GetCategoryById(categoryId);
        }

        public async Task<Result<List<NewsCategory>>> GetParent(int categoryId)
        {
            return await _newsCategoryRepository.GetParent(categoryId);
        }

        public async Task<Result> IsExist(int? cateId)
        {
            return await _newsCategoryRepository.IsExist(cateId);
        }

        public async Task<Result<ShowNewsCategoryViewModel>> UpdateCategory(UpdateNewsCategoryViewModel updateNewsCategoryViewModel)
        {
            try
            {
                //  var notfound = await _newsCategoryRepository.IsExist(updateNewsCategoryViewModel.parent);
                //   if (notfound.NotSucceeded)
                //       return Result.Failed<ShowNewsCategoryViewModel>("این ساب کتگوری وجود ندارد");

                //var exi = await _newsCategoryRepository.ExistCategory(updateNewsCategoryViewModel.CategoryTitle, updateNewsCategoryViewModel.parent ?? 0);
                //if (exi.Succeeded)
                //    return Result.Failed<ShowNewsCategoryViewModel>("تکراری است");

                var upNew = await _newsCategoryRepository.GetCategoryById(updateNewsCategoryViewModel.Id);
                if (upNew == null)
                    return Result.Failed<ShowNewsCategoryViewModel>("یافت نشد");

                var dto = new UpdateNewsCategoryDTO();

                dto.Id = updateNewsCategoryViewModel.Id;
                dto.CategoryTitle = updateNewsCategoryViewModel.CategoryTitle;
                // dto.parent = updateNewsCategoryViewModel.parent;

                var res = await _newsCategoryRepository.UpdateCategory(dto);


                var map = new ShowNewsCategoryViewModel();

                map.Id = res.Value.Id;
                map.Title = res.Value.Title;
                //map.parent = res.Value.parent;

                return Result.Success(map);
            }
            catch (Exception ex)
            {

                _logger.LogDebug(ex, "UpdateCategory");
                return Result.Failed<ShowNewsCategoryViewModel>("خطا در به روز رسانی!!");
            }

        }
    }
}

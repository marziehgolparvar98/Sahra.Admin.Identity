using Microsoft.Extensions.Logging;
using Sahara.Common;
using Sahra.DataLayer.IRepository;
using Sahra.DataLayer.Models.DTO;
using Sahra.DataLayer.Models.Entities;
using Sahra.Services.IService;
using Sahra.ViewModel.Category;
using Sahra.ViewModel.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sahra.Services.Service
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly INewsCategoryRepository _newsCategoryRepository;
        private readonly ILogger<NewsService> _logger;
        public NewsService(INewsRepository newsRepository, INewsCategoryRepository newsCategoryRepository, ILogger<NewsService> logger)
        {
            _newsRepository = newsRepository;
            _newsCategoryRepository = newsCategoryRepository;
            _logger = logger;
        }
        public async Task<Result<News>> AddNews(AddNewsViewModel addNews)
        {
            try
            {
                if (!UploadFileExtension.CheckIfImageFile(addNews.Image))
                    return Result.Failed<News>("فایل آپلود شده غیر مجاز است");

                var size = (float)addNews.Image.Length / 8F / 1024F / 1024F;
                if (size > 5)
                    return Result.Failed<News>("حجم  آپلود شده بیشتز از حد مجاز است");

                var exi = await _newsCategoryRepository.IsEndOfChain(addNews.CatId);

                if (exi.NotSucceeded)
                    return Result.Failed<News>("حق درج خبر در این کتگوری را ندارید ; آخرین زیر مجموعه ی کتگوری راانتخاب نمایید.");

                var nm = new News();
                nm.NewsCategoryId = addNews.CatId;
                nm.NewsDescription = addNews.Description;
                nm.Title = addNews.Title;
                if (addNews.Image.Length > 0)
                {
                    nm.Image = await UploadFileExtension.WriteFile("News", addNews.Image);
                }
                nm.CreateDate = addNews.CreateNewsDateTime;
                nm.NewsCreator = addNews.Creator;
                nm.CreatNews = DateTime.Now;
                nm.NewsSummary = addNews.NewsSummary;
                var result = await _newsRepository.AddNews(nm);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AddNews");
                return Result.Failed<News>("خطا در درج خبر !!");
            }
        }

        public async Task<Result<News>> DeleteNews(int id)
        {
            try
            {
                var post = await _newsRepository.GetNewsById(id);
                if (post.Value?.Image != null)
                    UploadFileExtension.DeleteFile("News", post.Value.Image);
                return await _newsRepository.DeleteNews(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteNews");
                return Result.Failed<News>("خطا در حذف خبر");
            }
        }

        public async Task<Result<List<ShowNewsViewModel>>> GetAllNews(int page, int pageSize)
        {
            try
            {
                var skip = pageSize * page - pageSize;
                var take = pageSize;
                if (page < 1 || pageSize < 1)
                {
                    skip = 1;
                    take = 100;
                }
                var res = await _newsRepository.GetAllNews(skip, take);
                var result = new List<ShowNewsViewModel>();
                var cats = await _newsCategoryRepository.GetAllCategory();
                foreach (var item in res.Value)
                {
                    var exi = new ShowNewsViewModel();
                    exi.CategoryTitle = item.NewsCategory.Title;
                    exi.CreateDate = item.CreateDate;
                    exi.Creator = item.NewsCreator;
                    exi.Id = item.Id;
                    exi.Image = item.Image;
                    exi.Description = item.NewsDescription;
                    exi.Title = item.Title;
                    exi.NewsSummary = item.NewsSummary;
                    exi.Category = new List<CategoryViewModel>();

                    int? xId = item.NewsCategory.Id;
                    int priority = 1;

                    List<int> cat = new List<int>();
                    while (xId != null)
                    {
                        if (cat.Any(x => x == xId))
                        {
                            _logger.LogError("Loop Category {@Category}", item);
                            break;
                        }

                        cat.Add(xId ?? 0);

                        var x = cats.Value.FirstOrDefault(current => current.Id == xId);
                        exi.Category.Add(new CategoryViewModel() { Id = x.Id, Title = x.Title, Priority = priority });
                        xId = x?.Parent;
                        priority++;

                    }
                    result.Add(exi);
                }
                return Result.Success(result);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "GetAllNews");
                return Result.Failed<List<ShowNewsViewModel>>("خطا در دریافت خبر");
            }
        }

        public async Task<Result<News>> GetNewsById(int id)
        {
            return await _newsRepository.GetNewsById(id);
        }

        public async Task<Result<News>> UpdateNews(UpdateNewsViewModel updateNewsViewModel)
        {
            try
            {
                var post = await _newsRepository.GetNewsById(updateNewsViewModel.Id);

                if (post.Value == null)
                    return Result.Failed<News>("یافت نشد");

                if (updateNewsViewModel.Image != null)
                {
                    UploadFileExtension.DeleteFile("News", post.Value.Image);
                    var newName = await UploadFileExtension.WriteFile("News", updateNewsViewModel.Image);
                    post.Value.Image = newName;
                }

                var dto = new UpdateNewsDTO();
                dto.Id = updateNewsViewModel.Id;
                dto.Description = updateNewsViewModel.NewsDescription;
                dto.Image = post.Value.Image;
                dto.NewsSummary = updateNewsViewModel.NewsSummary;
                dto.Title = updateNewsViewModel.Title;
                return await _newsRepository.UpdateNews(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateNews");
                return Result.Failed<News>("خطا در بروز رسانی خبر!!");
            }
        }
    }
}

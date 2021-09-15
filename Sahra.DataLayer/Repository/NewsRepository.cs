using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    public class NewsRepository : INewsRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<NewsRepository> _logger;
        public NewsRepository(ApplicationDbContext applicationDbContext, ILogger<NewsRepository> logger)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<Result<News>> AddNews(News news)
        {
            _applicationDbContext.News.Add(news);
            await _applicationDbContext.SaveChangesAsync();
            return Result.Success(news);

        }

        public async Task<Result<News>> DeleteNews(int id)
        {
            var news =
               await _applicationDbContext.News.FirstOrDefaultAsync(current => current.Id == id);

            if (news != null)
            {
                _applicationDbContext.News.Remove(news);
                await _applicationDbContext.SaveChangesAsync();
                return Result.Success(news);
            }
            return Result.Failed<News>("خبری با این آیدی یافت نشد!!");
        }

        public async Task<Result<List<News>>> GetAllNews(int skip, int take)
        {
            var result = await _applicationDbContext.News.Include(res => res.NewsCategory).OrderByDescending(x => x.Id).Skip(skip)
                .Take(take).ToListAsync();
            return Result.Success(result);
        }

        public async Task<Result<News>> GetNewsById(int id)
        {
            var result = await _applicationDbContext.News.Include(res => res.NewsCategory).FirstOrDefaultAsync(current => current.Id == id);
            return Result.Success(result);
            if(result == null)
                return Result.Failed<News>("خبری با این آیدی یافت نشد");
        }

        public async Task<Result<News>> UpdateNews(UpdateNewsDTO updateNewsDTO)
        {
            try
            {
                var upNews = await _applicationDbContext.News.FirstOrDefaultAsync(current => current.Id == updateNewsDTO.Id);
                if (upNews != null)
                {

                    upNews.Id = updateNewsDTO.Id;
                    upNews.Title = updateNewsDTO.Title;
                    upNews.NewsDescription = updateNewsDTO.Description;
                    upNews.Image = updateNewsDTO.Image;
                    upNews.NewsSummary = updateNewsDTO.NewsSummary;
                    _applicationDbContext.Entry(upNews).State = EntityState.Modified;
                    await _applicationDbContext.SaveChangesAsync();
                    return Result.Success(upNews);
                }
                return Result.Failed<News>("خبری با این آیدی یافت نشد!!");

            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "UpdateNews");
                return Result.Failed<News>("خطا در بروزرسانی خبر!!");
            }
        }
    }
}

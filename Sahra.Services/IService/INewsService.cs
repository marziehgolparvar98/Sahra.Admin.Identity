using Sahara.Common;
using Sahra.DataLayer.Models.Entities;
using Sahra.ViewModel.News;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahra.Services.IService
{
    public interface INewsService
    {
        Task<Result<List<ShowNewsViewModel>>> GetAllNews(int page, int pageSize);
        Task<Result<News>> GetNewsById(int id);
        Task<Result<News>> AddNews(AddNewsViewModel addNews);
        Task<Result<News>> UpdateNews(UpdateNewsViewModel updateNewsViewModel);
        Task<Result<News>> DeleteNews(int id);
    }
}

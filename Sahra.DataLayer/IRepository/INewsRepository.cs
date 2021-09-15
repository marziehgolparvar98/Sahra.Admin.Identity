using Sahara.Common;
using Sahra.DataLayer.Models.DTO;
using Sahra.DataLayer.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahra.DataLayer.IRepository
{
    public interface INewsRepository
    {
        Task<Result<List<News>>> GetAllNews(int skip, int take);
        Task<Result<News>> GetNewsById(int id);
        Task<Result<News>> AddNews(News news);
        Task<Result<News>> UpdateNews(UpdateNewsDTO updateNewsDTO);
        Task<Result<News>> DeleteNews(int id);
    }
}

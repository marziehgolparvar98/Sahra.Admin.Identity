using Sahara.Common;
using Sahra.DataLayer.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahra.DataLayer.IRepository
{
    public interface IUploadFileRepository
    {
        Task<Result<ESahraUploadFile>> GetFileById(int Id);
        Task<Result<List<ESahraUploadFile>>> GetAllFiles(int skip, int take);
        Task<Result<ESahraUploadFile>> DeleteFile(int id);
        Task<Result<ESahraUploadFile>> UploadFile(ESahraUploadFile uploadFile);
    }
}

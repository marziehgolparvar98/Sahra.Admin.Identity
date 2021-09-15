using Sahara.Common;
using Sahra.DataLayer.Models.Entities;
using Sahra.ViewModel.UploadFile;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahra.Services.IService
{
    public interface IUploadFileService
    {
        Task<Result<ESahraUploadFile>> UploadFile(UploadFileViewModel uploadFileViewModel);
        Task<Result<ESahraUploadFile>> DeleteFile(int id);
        Task<Result<List<ESahraUploadFile>>> GetAllFiles(int page, int pageSize);
        Task<Result<ESahraUploadFile>> GetFileById(int Id);
    }
}

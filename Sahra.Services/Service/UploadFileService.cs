using Microsoft.Extensions.Logging;
using Sahara.Common;
using Sahra.DataLayer.IRepository;
using Sahra.DataLayer.Models.Entities;
using Sahra.Services.IService;
using Sahra.ViewModel.UploadFile;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahra.Services.Service
{
    public class UploadFileService : IUploadFileService
    {
        private readonly IUploadFileRepository _uploadFileRepository;
        private readonly ILogger<UploadFileService> _logger;
        public UploadFileService(IUploadFileRepository uploadFileRepository, ILogger<UploadFileService> logger)
        {
            _uploadFileRepository = uploadFileRepository;
            _logger = logger;
        }
        public async Task<Result<ESahraUploadFile>> UploadFile(UploadFileViewModel uploadFileViewModel)
        {
            var uf = new ESahraUploadFile();
            if (!UploadFileExtension.CheckIfImageFile(uploadFileViewModel.UploadFile))
                return Result.Failed<ESahraUploadFile>("فایل آپلود شده غیر مجاز است");

            uf.CreateDate = DateTime.Now;
            if (uploadFileViewModel.UploadFile.Length > 0)
            {
                uf.Image = await UploadFileExtension.WriteFile("uploadFile", uploadFileViewModel.UploadFile);
            }
            var result = await _uploadFileRepository.UploadFile(uf);
            return Result.Success(uf);
        }
        public async Task<Result<ESahraUploadFile>> DeleteFile(int id)
        {
            var delUp = await _uploadFileRepository.GetFileById(id);
            if (delUp.Value?.Image != null)
                UploadFileExtension.DeleteFile("uploadFile", delUp.Value.Image);
            return await _uploadFileRepository.DeleteFile(id);
        }
        public async Task<Result<List<ESahraUploadFile>>> GetAllFiles(int page, int pageSize)
        {
            var skip = pageSize * page - pageSize;
            var take = pageSize;
            if (page < 1 || pageSize < 1)
            {
                skip = 1;
                take = 100;
            }

            return await _uploadFileRepository.GetAllFiles(skip, take);
        }

        public async Task<Result<ESahraUploadFile>> GetFileById(int Id)
        {
            return await _uploadFileRepository.GetFileById(Id);
        }
    }
}
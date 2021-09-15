using Microsoft.EntityFrameworkCore;
using Sahara.Common;
using Sahra.DataLayer.IRepository;
using Sahra.DataLayer.Models.DbContext;
using Sahra.DataLayer.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sahra.DataLayer.Repository
{
    public class UploadFileRepository : IUploadFileRepository
    {
        private readonly ESahraApplicationDbContext _eSahraApplicationDbContext;
        public UploadFileRepository(ESahraApplicationDbContext eSahraApplicationDbContext)
        {
            _eSahraApplicationDbContext = eSahraApplicationDbContext;
        }
        public async Task<Result<ESahraUploadFile>> UploadFile(ESahraUploadFile uploadFile)
        {
            await _eSahraApplicationDbContext.ESahraUploadFile.AddAsync(uploadFile);
            await _eSahraApplicationDbContext.SaveChangesAsync();
            return Result.Success(uploadFile);
        }

        public async Task<Result<ESahraUploadFile>> DeleteFile(int id)
        {
            try
            {
                var result = await _eSahraApplicationDbContext.ESahraUploadFile.FirstOrDefaultAsync(c => c.Id == id);
                if (result != null)
                {
                    _eSahraApplicationDbContext.ESahraUploadFile.Remove(result);
                    await _eSahraApplicationDbContext.SaveChangesAsync();
                    return Result.Success(result);
                }
                return Result.Failed<ESahraUploadFile>("فایلی با این آیدی یافت نشد.");
            }
            catch (System.Exception)
            {
                return Result.Failed<ESahraUploadFile>("خطا در حذف فایل.");
            }
        }

        public async Task<Result<List<ESahraUploadFile>>> GetAllFiles(int skip, int take)
        {
            var result = await _eSahraApplicationDbContext.ESahraUploadFile.OrderByDescending(x => x.Id).Skip(skip).Take(take).ToListAsync();
            return Result.Success(result);
        }

        public async Task<Result<ESahraUploadFile>> GetFileById(int Id)
        {
            var result = await _eSahraApplicationDbContext.ESahraUploadFile.FirstOrDefaultAsync(c => c.Id == Id);
            return Result.Success(result);
        }
    }
}

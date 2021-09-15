using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sahara.Common;
using Sahra.DataLayer.IRepository;
using Sahra.DataLayer.Models.DbContext;
using Sahra.DataLayer.Models.DTO.ResumeDTO;
using Sahra.DataLayer.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sahra.DataLayer.Repository
{
    public class ResumeRepository : IResumeRepository
    {
        private ESahraApplicationDbContext _eSahraApplicationDbContext;
        private ILogger<ResumeRepository> _logger;
        public ResumeRepository(ESahraApplicationDbContext eSahraApplicationDbContext, ILogger<ResumeRepository> logger)
        {
            _eSahraApplicationDbContext = eSahraApplicationDbContext;
            _logger = logger;
        }
        public async Task SaveAsync()
        {
            await _eSahraApplicationDbContext.SaveChangesAsync();
        }
        public async Task<Result<IList<Resume>>> GetAllResume()
        {
            IList<Resume> result = await _eSahraApplicationDbContext.Resumes.OrderByDescending(x => x.CreateDate).ToListAsync();
            if (result == null)
                return Result.Failed<IList<Resume>>("موردی یافت نشد !!");
            return Result.Success(result);

        }

        public async Task<Result<Resume>> GetResumeById(int id)
        {
            var result =
                await _eSahraApplicationDbContext.Resumes
                .FirstOrDefaultAsync(current => current.Id == id);
            if (result == null)
                return Result.Failed<Resume>("رزومه ای  با این آیدی یافت نشد !!");
            return Result.Success(result);
        }

        public async Task<Result<Resume>> AddResume(Resume resume)
        {
            try
            {
                var result = await _eSahraApplicationDbContext.Resumes.AddAsync(resume);
                await SaveAsync();
                return Result.Success(resume);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "RegisterInvestRequest");
                return Result.Failed<Resume>("ثبت نام نا موفق !!");
            }

        }

        public async Task<Result> UpdateResume(UpdateResumeDTO updateResume)
        {
            try
            {
                var rez = await _eSahraApplicationDbContext.Resumes.
                    FirstOrDefaultAsync(current => current.Id == updateResume.Id);
                if (rez != null)
                {
                    rez.LastName = updateResume.LastName;
                    rez.Mobile = updateResume.Mobile;
                    rez.Image = updateResume.UploadFile;
                    rez.Name = updateResume.Name;
                    rez.MetaData = updateResume.MetaData;
                    _eSahraApplicationDbContext.Entry(rez).State = EntityState.Modified;
                    await SaveAsync();
                    return Result.Success(rez);
                }
                return Result.Failed("با این آیدی موردی یافت نشد !!");

            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "UpdateResume");
                return Result.Failed("خطا !!");
            }
        }
    }
}

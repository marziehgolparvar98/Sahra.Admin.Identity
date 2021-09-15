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
    public class InvestRequestRepository : IInvestRequestRepository
    {
        private ApplicationDbContext _applicationDbContext;
        private ILogger<InvestRequestRepository> _logger;
        public InvestRequestRepository(ApplicationDbContext applicationDbContext, ILogger<InvestRequestRepository> logger)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
        }
        public async Task SaveAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }
        public async Task<Result<IList<InvestRequest>>> GetAllInvestRequest()
        {
            IList<InvestRequest> result = await _applicationDbContext.InvestRequests.OrderByDescending(x => x.CreateDate).ToListAsync();
            if (result == null)
                return Result.Failed<IList<InvestRequest>>("موردی یافت نشد !!");
            return Result.Success(result);

        }

        public async Task<Result<InvestRequest>> GetInvestRequestById(int id)
        {
            var result =
                await _applicationDbContext.InvestRequests
                .Include(result => result.Collabration)
                .Include(result => result.Members)
                .Include(result => result.Category)
                .Include(result => result.Platform)
                .Include(result => result.UploadFile)
                .FirstOrDefaultAsync(current => current.Id == id);
            if (result == null)
                return Result.Failed<InvestRequest>("کاربری با این آیدی یافت نشد !!");
            return Result.Success(result);
        }

        public async Task<Result<InvestRequest>> GetInvestRequestByTrackingNumber(int trackingNumber)
        {
            var result =
               await _applicationDbContext.InvestRequests
               .Include(result => result.Collabration)
               .Include(result => result.Members)
               .Include(result => result.Category)
               .Include(result => result.Platform)
               .FirstOrDefaultAsync(current => current.TrackingNumber == trackingNumber);
            if (result == null)
                return Result.Failed<InvestRequest>("کاربری با این کد رهگیری یافت نشد !!");
            return Result.Success(result);
        }

        public async Task<Result<InvestRequest>> RegisterInvestRequest(InvestRequest investRequest)
        {
            try
            {
                var result = await _applicationDbContext.InvestRequests.AddAsync(investRequest);
                await SaveAsync();
                return Result.Success(investRequest);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "RegisterInvestRequest");
                return Result.Failed<InvestRequest>("ثبت نام نا موفق !!");
            }

        }

        public async Task<Result> UpdateRequestStatus(ShowUpdateInvestRequestViewModel updateInvestRequestDTO)
        {
            try
            {
                var ir = await _applicationDbContext.InvestRequests.
                    FirstOrDefaultAsync(current => current.Id == updateInvestRequestDTO.Id);


                ir.RequestStatus = updateInvestRequestDTO.EnumRequestStatus;
                _applicationDbContext.Entry(ir).State = EntityState.Modified;
                await SaveAsync();
                return Result.Success(ir);

            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "UpdateRequestStatus");
                return Result.Failed("موردی یافت نشد !!");
            }
        }
    }
}

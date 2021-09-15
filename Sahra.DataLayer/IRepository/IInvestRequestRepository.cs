using Sahara.Common;
using Sahra.DataLayer.Models.DTO;
using Sahra.DataLayer.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahra.DataLayer.IRepository
{
    public interface IInvestRequestRepository
    {
        Task<Result<IList<InvestRequest>>> GetAllInvestRequest();
        Task<Result<InvestRequest>> GetInvestRequestById(int id);
        Task<Result<InvestRequest>> GetInvestRequestByTrackingNumber(int trackingNumber);
        Task<Result> UpdateRequestStatus(ShowUpdateInvestRequestViewModel updateInvestRequestDTO);
        Task<Result<InvestRequest>> RegisterInvestRequest(InvestRequest investRequest);
        Task SaveAsync();
    }
}

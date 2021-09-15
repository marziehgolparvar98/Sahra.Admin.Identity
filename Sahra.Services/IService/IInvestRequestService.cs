using Sahara.Common;
using Sahra.DataLayer.Models.DTO;
using Sahra.DataLayer.Models.Entities;
using Sahra.ViewModel.InvestRequest;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahra.Services.IService
{
    public interface IInvestRequestService
    {

        Task<Result<IList<ShowInvestRequestViewModel>>> GetAllInvestRequest();
        Task<Result<InvestRequest>> GetInvestRequestById(int id);
        Task<Result<InvestRequest>> GetInvestRequestByTrackingNumber(int trackingNumber);
        Task<Result<ShowAddInvestRequestViewModel>> RegisterInvestRequest(AddInvestRequestViewModel addInvestRequest);
        Task<Result<ShowUpdateInvestRequestViewModel>> UpdateStatus(ShowUpdateInvestRequestViewModel updateInvestRequestDTO);
    }
}

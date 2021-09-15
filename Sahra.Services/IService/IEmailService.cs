using Sahara.Common;
using Sahra.DataLayer.Models.Entities;
using Sahra.ViewModel.Mail;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahra.Services.IService
{
    public interface IEmailService
    {
        Task<Result<List<Email>>> GetAllEmail(int page, int pageSize);
        Task<Result<Email>> AddEmail(AddEmailViewModel addEmailViewModel);
    }
}

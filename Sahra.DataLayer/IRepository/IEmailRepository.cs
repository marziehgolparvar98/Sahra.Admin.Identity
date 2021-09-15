using Sahara.Common;
using Sahra.DataLayer.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahra.DataLayer.IRepository
{
    public interface IEmailRepository
    {
        Task<Result<List<Email>>> GetAllEmail(int skip, int take);
        Task<Result<Email>> AddEmail(Email email);
    }
}

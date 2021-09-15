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
    public class EmailRepository : IEmailRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public EmailRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<Result<Email>> AddEmail(Email email)
        {
            _applicationDbContext.Emails.Add(email);
            await _applicationDbContext.SaveChangesAsync();
            return Result.Success(email);
        }

        public async Task<Result<List<Email>>> GetAllEmail(int skip, int take)
        {
            var result = await _applicationDbContext.Emails.OrderByDescending(x => x.Id).Skip(skip).Take(take).ToListAsync();
            return Result.Success(result);
        }
    }
}

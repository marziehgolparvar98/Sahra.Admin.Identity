using Sahara.Common;
using Sahra.DataLayer.IRepository;
using Sahra.DataLayer.Models.Entities;
using Sahra.Services.IService;
using Sahra.ViewModel.Mail;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahra.Services.Service
{
    public class EmailService : IEmailService
    {
        private readonly IEmailRepository _emailRepository;
        public EmailService(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }
        public async Task<Result<Email>> AddEmail(AddEmailViewModel addEmailViewModel)
        {
            var ae = new Email();
            ae.CreateDate = DateTime.Now;
            ae.Description = addEmailViewModel.Description;
            ae.Mail = addEmailViewModel.Mail;
            var result =
               await _emailRepository.AddEmail(ae);
            return result;
        }

        public async Task<Result<List<Email>>> GetAllEmail(int page, int pageSize)
        {
            if (page < 1 || pageSize < 1)
            {
                page = 1;
                pageSize = 100;
            }
            var skip = pageSize * page - pageSize;
            var take = pageSize;

            return await _emailRepository.GetAllEmail(skip, take);
        }
    }
}

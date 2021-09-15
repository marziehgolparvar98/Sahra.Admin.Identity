using Sahara.Common;
using Sahra.DataLayer.Models.DTO.ResumeDTO;
using Sahra.ViewModel.Resume;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahra.Services.IService
{
    public interface IResumeService
    {
        Task<Result<List<GetResumeDTO>>> GetAllResume();
        Task<Result<GetResumeDTO>> GetResumeById(int id);
        Task<Result<GetResumeDTO>> AddResume(AddResume addResume);
        Task<Result<UpdateResumeDTO>> UpdateResume(UpdateResumeViewModel update);
    }
}

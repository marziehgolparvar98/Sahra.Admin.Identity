using Sahara.Common;
using Sahra.DataLayer.Models.DTO.ResumeDTO;
using Sahra.DataLayer.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahra.DataLayer.IRepository
{
    public interface IResumeRepository
    {
        Task SaveAsync();
        Task<Result<IList<Resume>>> GetAllResume();
        Task<Result<Resume>> GetResumeById(int id);
        Task<Result<Resume>> AddResume(Resume resume);
        Task<Result> UpdateResume(UpdateResumeDTO updateResume);
    }
}

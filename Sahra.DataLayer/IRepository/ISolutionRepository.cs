using Sahara.Common;
using Sahra.DataLayer.Models.DTO.SolutionDTO;
using Sahra.DataLayer.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahra.DataLayer.IRepository
{
    public interface ISolutionRepository
    {
        Task<Result<Solution>> AddSolution(Solution solution);
        Task<Result<Solution>> DeleteSolution(int id);
        Task<Result<List<Solution>>> GetAllSolution(int skip, int take);
        Task<Result<Solution>> GetSolutionById(int Id);
        Task<Result<Solution>> UpdateSolution(UpdateSolutionDTO updateSolutionDTO);
    }
}

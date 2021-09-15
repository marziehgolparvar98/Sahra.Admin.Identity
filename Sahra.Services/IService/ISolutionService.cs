using Sahara.Common;
using Sahra.DataLayer.Models.DTO.SolutionDTO;
using Sahra.DataLayer.Models.Entities;
using Sahra.ViewModel.Solution;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahra.Services.IService
{
    public interface ISolutionService
    {
        Task<Result<GetSolutionDTO>> AddSolution(AddSolution addSolution);
        Task<Result<Solution>> DeleteSolution(int id);
        Task<Result<List<GetSolutionDTO>>> GetAllSolution(int page, int pageSize);
        Task<Result<GetSolutionDTO>> GetSolutionById(int Id);
        Task<Result<Solution>> UpdateSoluton(UpdateSolutionViewModel updateSolutionViewModel);
    }
}

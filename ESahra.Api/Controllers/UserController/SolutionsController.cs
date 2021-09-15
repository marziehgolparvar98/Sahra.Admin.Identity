using Microsoft.AspNetCore.Mvc;
using Sahara.Common;
using Sahra.Services.IService;
using System.Threading.Tasks;

namespace ESahra.Api.Controllers.UserController
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolutionsController : ControllerBase
    {
        private readonly ISolutionService _solutionService;
        public SolutionsController(ISolutionService solutionService)
        {
            _solutionService = solutionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSolution(int page, int pageSize)
        {
            var result =
                await _solutionService.GetAllSolution(page, pageSize);
            return result.ToHttpCodeResult();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSolutionById(int id)
        {
            var result =
                await _solutionService.GetSolutionById(id);
            return result.ToHttpCodeResult();
        }
    }
}

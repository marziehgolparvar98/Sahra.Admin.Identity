using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Sahra.Services.IService;
using Sahara.Common;
using Sahra.ViewModel.Solution;
using Sahra.DataLayer.Models.MetaData.Solution;

namespace ESahra.Api.Controllers.AdminController
{

#if !DEBUG
[Authorize]
#endif

    [ApiController]
    [Route("api/admin/[controller]")]
    public class SolutionsController : ControllerBase
    {

        private readonly ISolutionService _solutionService;
        private readonly ILogger<SolutionsController> _logger;
        public SolutionsController(ISolutionService solutionService, ILogger<SolutionsController> logger)
        {
            _solutionService = solutionService;
            _logger = logger;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSolution(int id)
        {
            var result =
                  await _solutionService.DeleteSolution(id);
            return result.ToHttpCodeResult();
        }

        [HttpPost]
        public async Task<IActionResult> AddSolutions([FromForm] AddSolution addSolution)
        {
            try { addSolution.SolutionCustomer = Newtonsoft.Json.JsonConvert.DeserializeObject<SolutionCustomer[]>(Request.Form["SolutionCustomer"].ToString()); } catch (Exception ex) { _logger.LogError(ex, "Parsing From Data Request"); }
            try { addSolution.SolutionDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<SolutionDetails[]>(Request.Form["SolutionDetails"].ToString()); } catch (Exception ex) { _logger.LogError(ex, "Parsing From Data Request"); }
            try { addSolution.SolutionSubTitles = Newtonsoft.Json.JsonConvert.DeserializeObject<SolutionSubTitles[]>(Request.Form["SolutionSubTitles"].ToString()); } catch (Exception ex) { _logger.LogError(ex, "Parsing From Data Request"); }

            var res = await _solutionService.AddSolution(addSolution);
            return res.ToHttpCodeResult();
        }
    }
}
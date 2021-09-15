using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sahara.Common;
using Sahra.Services.IService;
using Sahra.ViewModel.InvestRequest;
using System;
using System.Threading.Tasks;

namespace Sahra.Accelerator.api.Controllers.UserController
{
    [Route("api/[controller]")]
    [ApiController]

    public class InvestRequestController : ControllerBase
    {
        private readonly IInvestRequestService _investRequestService;
        private readonly ILogger<InvestRequestController> _logger;

        public InvestRequestController(IInvestRequestService investRequestService, ILogger<InvestRequestController> logger)
        {
            _investRequestService = investRequestService;
            _logger = logger;
        }

        [HttpPost("Submit")]

        // [Captcha]



        public async Task<IActionResult> RegisterInvestRequest([FromForm] AddInvestRequestViewModel addInvestRequest)
        {


            try { addInvestRequest.Members = Newtonsoft.Json.JsonConvert.DeserializeObject<ShowMembersViewModel[]>(Request.Form["Members"].ToString()); } catch (Exception ex) { _logger.LogError(ex, "Parsing From Data Request"); }
            try { addInvestRequest.Category = Newtonsoft.Json.JsonConvert.DeserializeObject<ShowCategoryViewModel[]>(Request.Form["Category"].ToString()); } catch (Exception ex) { _logger.LogError(ex, "Parsing From Data Request"); }
            try { addInvestRequest.Platform = Newtonsoft.Json.JsonConvert.DeserializeObject<ShowPlatformViewModel[]>(Request.Form["Platform"].ToString()); } catch (Exception ex) { _logger.LogError(ex, "Parsing From Data Request"); }
            try { addInvestRequest.Collabration = Newtonsoft.Json.JsonConvert.DeserializeObject<ShowCollabrationViewModel[]>(Request.Form["Collabration"].ToString()); } catch (Exception ex) { _logger.LogError(ex, "Parsing From Data Request"); }

            var result = await _investRequestService.RegisterInvestRequest(addInvestRequest);
            return result.ToHttpCodeResult();

        }
    }
}

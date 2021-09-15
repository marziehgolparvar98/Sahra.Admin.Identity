using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sahara.Common;
using Sahra.DataLayer.Models.DTO;
using Sahra.Services.IService;
using System;
using System.Threading.Tasks;

namespace Sahra.Accelerator.api.Controllers.AdminController
{
#if !DEBUG
[Authorize(Roles = UserRole.Admin)]
#endif
    [ApiExplorerSettings(GroupName = "AdminV1")]
    [ApiController]
    [Route("api/admin/[controller]")]
    public class InvestRequestController : ControllerBase
    {

        private readonly IInvestRequestService _investRequestService;
        private readonly ILogger<InvestRequestController> _logger;
        public InvestRequestController(IInvestRequestService investRequestService, ILogger<InvestRequestController> logger)
        {
            _investRequestService = investRequestService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllInvestRequest()
        {
            var result = await _investRequestService.GetAllInvestRequest();
            return result.ToHttpCodeResult();
        }

        [HttpGet("GetInvestRequestById")]
        public async Task<IActionResult> GetInvestRequestById(int id)
        {
            var result =
                await _investRequestService.GetInvestRequestById(id);
            return result.ToHttpCodeResult();
        }

        [HttpGet("GetInvestRequestByTrackingNumber")]
        public async Task<IActionResult> GetInvestRequestByTrackingNumber(int trackingNumber)
        {
            var result =
                await _investRequestService.GetInvestRequestByTrackingNumber(trackingNumber);
            return result.ToHttpCodeResult();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStatus(ShowUpdateInvestRequestViewModel updateInvestRequestDTO)
        {
            var result =
                await _investRequestService.UpdateStatus(updateInvestRequestDTO);
            return result.ToHttpCodeResult();
        }

        [HttpGet("File/Filename")]
        public async Task<IActionResult> GetFile(string fileName)
        {
            try
            {
                var filePathResult = UploadFileExtension.ReadFile("InvestRequest", fileName);
                if (filePathResult == null)
                    return NotFound();
                return File(filePathResult, UploadFileExtension.GetMimeType(fileName));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetImegs");
                return NotFound();
            }


        }

    }
}

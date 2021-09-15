using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sahra.Services.IService;
using Sahra.ViewModel.UploadFile;
using Sahara.Common;

namespace ESahra.Api.Controllers.AdminController
{
#if !DEBUG
[Authorize]
#endif
    [ApiController]
    [Route("api/admin/[controller]")]

    public class UploadFileController : ControllerBase
    {
        private readonly IUploadFileService _uploadFileService;
        public UploadFileController(IUploadFileService uploadFileService)
        {
            _uploadFileService = uploadFileService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var result =
                 await _uploadFileService.DeleteFile(id);
            return result.ToHttpCodeResult();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile([FromForm] UploadFileViewModel uploadFileViewModel)
        {
            var response = await _uploadFileService.UploadFile(uploadFileViewModel);
            return response.ToHttpCodeResult();
        }
    }
}



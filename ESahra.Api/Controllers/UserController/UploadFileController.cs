using Microsoft.AspNetCore.Mvc;
using Sahara.Common;
using Sahra.DataLayer.IRepository;
using System;
using System.Threading.Tasks;

namespace ESahra.Api.Controllers.UserController
{
    [Route("api/[controller]")]
    [ApiController]

    public class UploadFileController : ControllerBase
    {
        private readonly IUploadFileRepository _uploadFileRepository;
        public UploadFileController(IUploadFileRepository uploadFileRepository)
        {
            _uploadFileRepository = uploadFileRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFiles(int skip, int take)
        {
            var result =
                 await _uploadFileRepository.GetAllFiles(skip, take);
            return result.ToHttpCodeResult();
        }

        [HttpGet("GetSliderById")]
        public async Task<IActionResult> GetFileById(int id)
        {
            var result =
            await _uploadFileRepository.GetFileById(id);
            return result.ToHttpCodeResult();
        }

        [HttpGet("images/{imagename}")]
        public IActionResult GetFile(string imagename)
        {
            try
            {
                var imagePathResult = UploadFileExtension.ReadFile("uploadFile", imagename);
                if (imagePathResult == null)
                    return NotFound();
                return File(imagePathResult, UploadFileExtension.GetMimeType(imagename));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
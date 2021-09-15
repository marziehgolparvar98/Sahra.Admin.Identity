using Microsoft.AspNetCore.Mvc;
using Sahara.Common;
using Sahra.Services.IService;
using System;
using System.Threading.Tasks;

namespace ESahra.Api.Controllers.UserController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduct(int page, int pageSize)
        {
            var result =
                     await _productService.GetAllProduct(page, pageSize);
            return result.ToHttpCodeResult();
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProductById(int Id)
        {
            var result =
            await _productService.GetProductById(Id);
            return result.ToHttpCodeResult();
        }
        [HttpGet("EnProductName")]
        public async Task<IActionResult> GetProductByEnProductName(string enProductName)
        {
            var result =
            await _productService.GetProductByEnProductName(enProductName);
            return result.ToHttpCodeResult();
        }

        [HttpGet("images/{imagename}")]
        public IActionResult GetImegs(string imagename)
        {
            try
            {
                var imagePathResult = UploadFileExtension.ReadFile("Product", imagename);
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


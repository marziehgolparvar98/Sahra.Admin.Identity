using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sahara.Common;
using Sahra.Services.IService;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sahra.ViewModel.ESahraProduct;
using Sahra.DataLayer.Models.MetaData.Product;

namespace ESahra.Api.Controllers.AdminController
{
#if !DEBUG
[Authorize]
#endif 
    [ApiController]
    [Route("api/admin/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result =
                  await _productService.DeleteProduct(id);
            return result.ToHttpCodeResult();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductViewModel updateProductViewModel)
        {
            var result =
                    await _productService.UpdateProduct(updateProductViewModel);
            return result.ToHttpCodeResult();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] AddESahraProduct addESahraProduct)
        {
            try { addESahraProduct.Specifications = Newtonsoft.Json.JsonConvert.DeserializeObject<Specifications[]>(Request.Form["Specifications"].ToString()); } catch (Exception ex) { _logger.LogError(ex, "Parsing From Data Request"); }
            try { addESahraProduct.SubTitles = Newtonsoft.Json.JsonConvert.DeserializeObject<SubTitle[]>(Request.Form["SubTitles"].ToString()); } catch (Exception ex) { _logger.LogError(ex, "Parsing From Data Request"); }

            var response = await _productService.AddProduct(addESahraProduct);
            return response.ToHttpCodeResult();
        }
    }
}
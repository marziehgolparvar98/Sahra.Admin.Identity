using Sahara.Common;
using Sahra.DataLayer.Models.DTO.ProductDTO;
using Sahra.DataLayer.Models.Entities;
using Sahra.ViewModel.ESahraProduct;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahra.Services.IService
{
    public interface IProductService
    {
        Task<Result<GetProductDTO>> AddProduct(AddESahraProduct addESahraProduct);
        Task<Result<ESahraProduct>> DeleteProduct(int id);
        Task<Result<List<GetProductDTO>>> GetAllProduct(int page, int pageSize);
        Task<Result<GetProductDTO>> GetProductById(int Id);
        Task<Result<ESahraProduct>> GetProductByEnProductName(string enProductName);
        Task<Result<ESahraProduct>> UpdateProduct(UpdateProductViewModel updateProductViewModel);
    }
}

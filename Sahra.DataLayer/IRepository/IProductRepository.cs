using Sahara.Common;
using Sahra.DataLayer.Models.DTO.ProductDTO;
using Sahra.DataLayer.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahra.DataLayer.IRepository
{
    public interface IProductRepository
    {
        Task<Result<ESahraProduct>> AddProduct(ESahraProduct product);
        Task<Result<ESahraProduct>> DeleteProduct(int id);
        Task<Result<List<ESahraProduct>>> GetAllProduct(int skip, int take);
        Task<Result<ESahraProduct>> UpdateProduct(UpdateProductDTO updateProductDTO);
        Task<Result<ESahraProduct>> GetProductById(int Id);
        Task<Result<ESahraProduct>> GetProductByEnProductName(string enProductName);
    }
}

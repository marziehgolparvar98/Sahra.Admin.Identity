using Microsoft.EntityFrameworkCore;
using Sahara.Common;
using Sahra.DataLayer.IRepository;
using Sahra.DataLayer.Models.DbContext;
using Sahra.DataLayer.Models.DTO.ProductDTO;
using Sahra.DataLayer.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sahra.DataLayer.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ESahraApplicationDbContext _eSahraApplicationDbContext;
        public ProductRepository(ESahraApplicationDbContext eSahraApplicationDbContext)
        {
            _eSahraApplicationDbContext = eSahraApplicationDbContext;

        }

        public async Task<Result<ESahraProduct>> AddProduct(ESahraProduct product)
        {
            await _eSahraApplicationDbContext.ESahraProducts.AddAsync(product);
            await _eSahraApplicationDbContext.SaveChangesAsync();
            return Result.Success(product);
        }

        public async Task<Result<ESahraProduct>> DeleteProduct(int id)
        {
            try
            {
                var result = await _eSahraApplicationDbContext.ESahraProducts.FirstOrDefaultAsync(c => c.Id == id);
                if (result != null)
                {
                    _eSahraApplicationDbContext.ESahraProducts.Remove(result);
                    await _eSahraApplicationDbContext.SaveChangesAsync();
                    return Result.Success(result);
                }
                return Result.Failed<ESahraProduct>("محصولی با این آیدی یافت نشد.");
            }
            catch (System.Exception)
            {
                return Result.Failed<ESahraProduct>("خطا در حذف محصول.");
            }
        }

        public async Task<Result<List<ESahraProduct>>> GetAllProduct(int skip, int take)
        {
            var result = await _eSahraApplicationDbContext.ESahraProducts.OrderByDescending(x => x.Id).OrderByDescending(y => y.ProductType).Skip(skip).Take(take).ToListAsync();
            return Result.Success(result);
        }

        public async Task<Result<ESahraProduct>> GetProductByEnProductName(string enProductName)
        {
            var result = await _eSahraApplicationDbContext.ESahraProducts.FirstOrDefaultAsync(c => c.EnProductName == enProductName);
            return Result.Success(result);
        }

        public async Task<Result<ESahraProduct>> GetProductById(int Id)
        {
            var result = await _eSahraApplicationDbContext.ESahraProducts.FirstOrDefaultAsync(c => c.Id == Id);
            return Result.Success(result);
        }

        public async Task<Result<ESahraProduct>> UpdateProduct(UpdateProductDTO updateProductDTO)
        {
            try
            {
                var upPro = await _eSahraApplicationDbContext.ESahraProducts.FirstOrDefaultAsync(current => current.Id == updateProductDTO.Id);
                if (upPro != null)
                {
                    upPro.Title = updateProductDTO.Title;
                    upPro.EnProductName = updateProductDTO.EnProductName;
                    upPro.ProductName = updateProductDTO.ProductName;
                    upPro.ProductType = updateProductDTO.PruductType;
                    upPro.Summary = updateProductDTO.Summary;
                    upPro.Image = updateProductDTO.Image;
                    upPro.MetaData = updateProductDTO.MetaData;

                    await _eSahraApplicationDbContext.SaveChangesAsync();
                    return Result.Success(upPro);
                }
                return Result.Failed<ESahraProduct>("تغییری اعمال نشد.");
            }
            catch (System.Exception)
            {

                return Result.Failed<ESahraProduct>("خطا در بروز رسانی محصول!!");
            }
        }
    }
}

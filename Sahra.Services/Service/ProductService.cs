using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sahara.Common;
using Sahra.DataLayer.IRepository;
using Sahra.DataLayer.Models.DTO.ProductDTO;
using Sahra.DataLayer.Models.Entities;
using Sahra.DataLayer.Models.MetaData.Product;
using Sahra.Services.IService;
using Sahra.ViewModel.ESahraProduct;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahra.Services.Service
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }
        public async Task<Result<GetProductDTO>> AddProduct(AddESahraProduct addESahraProduct)
        {
            try
            {
                if (!UploadFileExtension.CheckIfImageFile(addESahraProduct.Image))
                    return Result.Failed<GetProductDTO>("عکس آپلود شده غیر مجاز است");
                var aEP = new ESahraProduct();
                aEP.Title = addESahraProduct.Title;
                aEP.ProductName = addESahraProduct.ProductName;
                aEP.ProductType = addESahraProduct.PruductType;
                aEP.CreateDate = DateTime.Now;
                aEP.EnProductName = addESahraProduct.EnProductName;
                aEP.Summary = addESahraProduct.Summary;

                if (addESahraProduct.Image.Length > 0)
                    aEP.Image = await UploadFileExtension.WriteFile("Product", addESahraProduct.Image);


                var mtd = new ProductMetaDataDTO();

                mtd.Specifications = new List<Specifications>();
                foreach (var item in addESahraProduct.Specifications)
                {
                    var spec = new Specifications();
                    spec.Description = item.Description;
                    spec.EnSpecificationName = item.EnSpecificationName;
                    spec.FaSpecificationName = item.FaSpecificationName;
                    spec.Image = item.Image;
                    spec.Capability = item.Capability;
                    spec.KeyBenefits = item.KeyBenefits;

                    mtd.Specifications.Add(spec);
                }



                mtd.SubTitles = addESahraProduct.SubTitles;
                aEP.MetaData = JsonConvert.SerializeObject(mtd);

                var result = await _productRepository.AddProduct(aEP);

                var map = new GetProductDTO();
                map.Id = result.Value.Id;
                map.CreateDate = result.Value.CreateDate;
                map.Title = result.Value.Title;
                map.ProductName = result.Value.ProductName;
                map.PruductType = result.Value.ProductType;
                map.CreateDate = result.Value.CreateDate;
                map.EnProductName = result.Value.EnProductName;
                map.Summary = result.Value.Summary;
                map.Image = result.Value.Image;
                if (result.Value.MetaData != null)
                {
                    var meta = JsonConvert.DeserializeObject<ProductMetaDataDTO>(result.Value.MetaData);

                    if (meta.Specifications != null)
                    {
                        map.Specifications = meta.Specifications;

                        if (meta.Capability != null)
                        {
                            map.Capability = meta.Capability;
                        }

                        if (meta.KeyBenefits != null)
                        {
                            map.KeyBenefits = meta.KeyBenefits;
                        }
                    }

                    if (meta.SubTitles != null)
                    {
                        map.SubTitles = meta.SubTitles;
                    }
                }
                aEP.MetaData = JsonConvert.SerializeObject(mtd);

                return Result.Success(map);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "AddProduct");
                return Result.Failed<GetProductDTO>("خطا در ثبت محصول!!");
            }
        }

        public async Task<Result<ESahraProduct>> DeleteProduct(int id)
        {
            try
            {

                var delPro = await _productRepository.GetProductById(id);
                if (delPro.Value?.Image != null)
                    UploadFileExtension.DeleteFile("Product", delPro.Value.Image);

                return await _productRepository.DeleteProduct(id);
            }
            catch (Exception)
            {

                return Result.Failed<ESahraProduct>("خطا در حذف محصول!!");
            }
        }

        public async Task<Result<List<GetProductDTO>>> GetAllProduct(int page, int pageSize)
        {
            try
            {
                var eSP = new List<GetProductDTO>();
                var skip = pageSize * page - pageSize;
                var take = pageSize;
                if (page < 1 || pageSize < 1)
                {
                    skip = 1;
                    take = 100;
                }

                var res = await _productRepository.GetAllProduct(skip, take);
                var result = new List<ESahraProduct>();
                foreach (var item in res.Value)
                {
                    var exi = new GetProductDTO();
                    exi.Id = item.Id;
                    exi.CreateDate = item.CreateDate;
                    exi.EnProductName = item.EnProductName;
                    exi.ProductName = item.ProductName;
                    exi.Summary = item.Summary;
                    exi.Title = item.Title;
                    exi.Image = item.Image;
                    exi.PruductType = item.ProductType;
                    if (item.MetaData != null)
                    {
                        var meta = JsonConvert.DeserializeObject<ProductMetaDataDTO>(item.MetaData);

                        if (meta.Capability != null)
                        {
                            exi.Capability = meta.Capability;
                        }

                        if (meta.Capability != null)
                        {
                            exi.KeyBenefits = meta.KeyBenefits;
                        }

                        if (meta.Specifications != null)
                        {
                            exi.Specifications = meta.Specifications;
                        }

                        if (meta.SubTitles != null)
                        {
                            exi.SubTitles = meta.SubTitles;
                        }
                    }
                    eSP.Add(exi);
                }
                _logger.LogInformation("GetAllProduct");
                return Result.Success(eSP);
            }

            catch (Exception ex)
            {

                _logger.LogInformation(ex, "GetAllProduct");

                return Result.Failed<List<GetProductDTO>>("خطا در دریافت محصول!!");
            }
        }

        public async Task<Result<ESahraProduct>> GetProductByEnProductName(string enProductName)
        {
            return await _productRepository.GetProductByEnProductName(enProductName);
        }

        public async Task<Result<GetProductDTO>> GetProductById(int Id)
        {
            var res = await _productRepository.GetProductById(Id);
            var exi = new GetProductDTO();

            exi.EnProductName = res.Value.EnProductName;
            exi.Id = res.Value.Id;
            exi.CreateDate = res.Value.CreateDate;
            exi.ProductName = res.Value.ProductName;
            exi.Summary = res.Value.Summary;
            exi.Title = res.Value.Title;
            exi.PruductType = res.Value.ProductType;
            exi.Image = res.Value.Image;
            if (res.Value.MetaData != null)
            {
                var meta = JsonConvert.DeserializeObject<ProductMetaDataDTO>(res.Value.MetaData);

                if (meta.Capability != null)
                {
                    exi.Capability = meta.Capability;
                }

                if (meta.Capability != null)
                {
                    exi.KeyBenefits = meta.KeyBenefits;
                }

                if (meta.Specifications != null)
                {
                    exi.Specifications = meta.Specifications;
                }
                if (meta.SubTitles != null)
                {
                    exi.SubTitles = meta.SubTitles;
                }
            }
            return Result.Success(exi);
        }

        public async Task<Result<ESahraProduct>> UpdateProduct(UpdateProductViewModel updateProductViewModel)
        {
            try
            {
                var upPro = await _productRepository.GetProductById(updateProductViewModel.Id);

                if (upPro == null)
                    return Result.Failed<ESahraProduct>("یافت نشد");

                if (updateProductViewModel.Image != null)
                {
                    UploadFileExtension.DeleteFile("Product", upPro.Value.Image);
                    var newName = await UploadFileExtension.WriteFile("Product", updateProductViewModel.Image);
                    upPro.Value.Image = newName;
                }

                var dto = new UpdateProductDTO();

                var mtd = new ProductMetaDataDTO();
                mtd.Capability = updateProductViewModel.Capability;
                mtd.Specifications = updateProductViewModel.Specifications;
                mtd.SubTitles = updateProductViewModel.SubTitles;
                mtd.KeyBenefits = updateProductViewModel.KeyBenefits;

                dto.MetaData = JsonConvert.SerializeObject(mtd);
                dto.Id = updateProductViewModel.Id;
                dto.Title = updateProductViewModel.Title;
                dto.Summary = updateProductViewModel.Summary;
                dto.EnProductName = upPro.Value.EnProductName;
                dto.ProductName = updateProductViewModel.ProductName;
                dto.PruductType = updateProductViewModel.PruductType;
                dto.Image = upPro.Value.Image;


                return await _productRepository.UpdateProduct(dto);
            }
            catch (Exception)
            {
                return Result.Failed<ESahraProduct>("خطا در به روز رسانی!!");
            }
        }
    }
}

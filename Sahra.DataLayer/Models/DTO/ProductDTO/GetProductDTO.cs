using Sahra.DataLayer.Models.Enums;
using Sahra.DataLayer.Models.MetaData.Product;
using System;
using System.Collections.Generic;

namespace Sahra.DataLayer.Models.DTO.ProductDTO
{
    public class GetProductDTO
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string ProductName { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public ProductType PruductType { get; set; }
        public string EnProductName { get; set; }
        public string Image { get; set; }
        public ICollection<SubTitle> SubTitles { get; set; }
        public ICollection<Capability> Capability { get; set; }
        public ICollection<KeyBenefits> KeyBenefits { get; set; }
        public ICollection<Specifications> Specifications { get; set; }
    }
}

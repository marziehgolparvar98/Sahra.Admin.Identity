using Sahra.DataLayer.Models.MetaData.Product;
using System.Collections.Generic;

namespace Sahra.DataLayer.Models.DTO.ProductDTO
{
    public class ProductMetaDataDTO
    {
        public ICollection<SubTitle> SubTitles { get; set; }
        public ICollection<Capability> Capability { get; set; }
        public ICollection<KeyBenefits> KeyBenefits { get; set; }
        public ICollection<Specifications> Specifications { get; set; }
    }
}

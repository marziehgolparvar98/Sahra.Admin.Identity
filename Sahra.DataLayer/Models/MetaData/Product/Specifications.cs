using System.Collections.Generic;

namespace Sahra.DataLayer.Models.MetaData.Product
{
    public class Specifications
    {
        public string EnSpecificationName { get; set; }
        public string FaSpecificationName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public ICollection<Capability> Capability { get; set; }
        public ICollection<KeyBenefits> KeyBenefits { get; set; }

    }
}

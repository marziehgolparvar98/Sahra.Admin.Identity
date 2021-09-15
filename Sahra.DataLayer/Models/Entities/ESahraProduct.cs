using Sahra.DataLayer.Models.Enums;

namespace Sahra.DataLayer.Models.Entities
{
    public class ESahraProduct : BaseESahra
    {
        public string ProductName { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public ProductType ProductType { get; set; }
        public string EnProductName { get; set; }
    }
}
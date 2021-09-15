using Sahra.DataLayer.Models.Enums;

namespace Sahra.DataLayer.Models.DTO.ProductDTO
{
    public class UpdateProductDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public ProductType PruductType { get; set; }
        public string EnProductName { get; set; }
        public string Image { get; set; }
        public string MetaData { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using Sahra.DataLayer.Models.Enums;
using Sahra.DataLayer.Models.MetaData.Product;
using System.Collections.Generic;

namespace Sahra.ViewModel.ESahraProduct
{
    public class AddESahraProduct
    {
        public string ProductName { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public ProductType PruductType { get; set; }
        public string EnProductName { get; set; }
        public IFormFile Image { get; set; }
        public ICollection<SubTitle> SubTitles { get; set; }

        public ICollection<Specifications> Specifications { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using System;

namespace Sahra.ViewModel.News
{
    public class AddNewsViewModel
    {
        public int CatId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public IFormFile Image { get; set; }
        public string Creator { get; set; }
        public DateTime CreateNewsDateTime { get; set; }
        public string NewsSummary { get; set; }

    }
}

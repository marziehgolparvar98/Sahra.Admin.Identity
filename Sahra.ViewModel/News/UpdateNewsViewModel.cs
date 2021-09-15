using Microsoft.AspNetCore.Http;

namespace Sahra.ViewModel.News
{
    public class UpdateNewsViewModel
    {
        public int Id { get; set; }
        public string NewsDescription { get; set; }
        public string Title { get; set; }
        public IFormFile Image { get; set; }
        public string NewsSummary { get; set; }
    }
}

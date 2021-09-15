using Sahra.ViewModel.Category;
using System;
using System.Collections.Generic;

namespace Sahra.ViewModel.News
{
    public class ShowNewsViewModel
    {
        public int Id { get; set; }
        public string CategoryTitle { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Creator { get; set; }
        public DateTime CreateDate { get; set; }
        public string NewsSummary { get; set; }
        public List<CategoryViewModel> Category { get; set; }
    }
}

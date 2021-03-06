using Microsoft.AspNetCore.Http;
using System;

namespace Sahra.ViewModel.Event
{
    public class UpdateEventViewModel
    {
        public int Id { get; set; }
        public IFormFile Image { get; set; }
        public string title { get; set; }
        public string Holder { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public bool IsOnline { get; set; }
        public string Description { get; set; }
    }
}

using System;

namespace Sahra.DataLayer.Models.DTO
{
    public class UpdateEventDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Holder { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public bool IsOnline { get; set; }
        public string Description { get; set; }
    }
}

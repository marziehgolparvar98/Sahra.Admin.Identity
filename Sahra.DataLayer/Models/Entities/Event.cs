using System;

namespace Sahra.DataLayer.Models.Entities
{
    public class Event : BaseProp
    {
        public string Description { get; set; }
        public string Holder { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public bool IsOnline { get; set; }
    }
}

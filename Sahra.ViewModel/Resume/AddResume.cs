using Microsoft.AspNetCore.Http;
using Sahra.DataLayer.Models.Enums;
using System.Collections.Generic;

namespace Sahra.ViewModel.Resume
{
    public class AddResume
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public IFormFile UploadFile { get; set; }
        public ICollection<JobType> JobType { get; set; }
    }
}

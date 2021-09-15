using Sahra.DataLayer.Models.Enums;
using System;
using System.Collections.Generic;

namespace Sahra.DataLayer.Models.DTO.ResumeDTO
{
    public class GetResumeDTO
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string UploadFile { get; set; }
        public ICollection<JobType> JobType { get; set; }
    }
}

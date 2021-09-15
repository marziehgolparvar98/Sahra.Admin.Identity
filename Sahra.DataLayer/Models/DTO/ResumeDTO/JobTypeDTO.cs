using Sahra.DataLayer.Models.Enums;
using System.Collections.Generic;

namespace Sahra.DataLayer.Models.DTO.ResumeDTO
{
    public class JobTypeDTO
    {
        public ICollection<JobType> JobType { get; set; }
    }
}

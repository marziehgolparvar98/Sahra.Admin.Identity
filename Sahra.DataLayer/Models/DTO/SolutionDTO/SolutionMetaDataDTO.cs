using Sahra.DataLayer.Models.MetaData.Solution;
using System.Collections.Generic;

namespace Sahra.DataLayer.Models.DTO.SolutionDTO
{
    public class SolutionMetaDataDTO
    {
        public ICollection<SolutionSubTitles> SolutionSubTitles { get; set; }
        public ICollection<SolutionCustomer> SolutionCustomer { get; set; }
        public ICollection<SolutionDetails> SolutionDetails { get; set; }
    }
}

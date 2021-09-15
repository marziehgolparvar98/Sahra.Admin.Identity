using Sahra.DataLayer.Models.MetaData.Solution;
using System;
using System.Collections.Generic;

namespace Sahra.ViewModel.Solution
{
    public class UpdateSolutionViewModel
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string SolutionName { get; set; }
        public ICollection<SolutionSubTitles> SolutionSubTitles { get; set; }
        public ICollection<SolutionCustomer> SolutionCustomer { get; set; }
        public ICollection<SolutionDetails> SolutionDetails { get; set; }
    }
}

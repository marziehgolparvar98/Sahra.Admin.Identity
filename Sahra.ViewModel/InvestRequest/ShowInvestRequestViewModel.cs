using Sahra.DataLayer.Models.Enums;
using System;
using System.Collections.Generic;

namespace Sahra.ViewModel.InvestRequest
{
    public class ShowInvestRequestViewModel
    {
        public int Id { get; set; }
        public DateTime CreatDate { get; set; }
        public string Name { get; set; }
        public string AgentName { get; set; }
        public string StartUpEmail { get; set; }
        public string Mobile { get; set; }
        public int TeamPersonCount { get; set; }
        public bool IsRegister { get; set; }
        public string Description { get; set; }
        public string Vision { get; set; }
        public int TrackingNumber { get; set; }
        public string WebSiteAddress { get; set; }
        public bool HasProductIdea { get; set; }
        public bool IsActiveUser { get; set; }
        public string Need { get; set; }
        public EnumRequestStatus RequestStatus { get; set; }
        public string Invest { get; set; }
        public ICollection<ShowUploadFileViewModel> UploadFile { get; set; }
        public List<ShowCategoryViewModel> Category { get; set; }
        public List<ShowCollabrationViewModel> Collabration { get; set; }
        public List<ShowPlatformViewModel> Platform { get; set; }
        public List<ShowMembersViewModel> Members { get; set; }

    }
}

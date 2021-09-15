using Microsoft.AspNetCore.Http;
using Sahra.Common;
using System.Collections.Generic;

namespace Sahra.ViewModel.InvestRequest
{
    public class AddInvestRequestViewModel : ICaptcha
    {
        public CaptchaItem Captcha { get; set; }

        public string Name { get; set; }
        public string AgentName { get; set; }
        public string StartUpEmail { get; set; }
        public string Mobile { get; set; }
        public int TeamPersonCount { get; set; }
        public bool IsRegister { get; set; }
        public string Description { get; set; }
        public string Vision { get; set; }
        public string WebSiteAddress { get; set; }
        public bool HasProductIdea { get; set; }
        public bool IsActiveUser { get; set; }
        public string Need { get; set; }
        public string Invest { get; set; }
        public ICollection<IFormFile> UploadFile { get; set; }
        public ICollection<ShowCategoryViewModel> Category { get; set; }
        public ICollection<ShowCollabrationViewModel> Collabration { get; set; }
        public ICollection<ShowPlatformViewModel> Platform { get; set; }
        public ICollection<ShowMembersViewModel> Members { get; set; }

    }
}

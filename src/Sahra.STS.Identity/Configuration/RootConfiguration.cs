using Skoruba.IdentityServer4.Shared.Configuration.Configuration.Identity;
using Sahra.STS.Identity.Configuration.Interfaces;

namespace Sahra.STS.Identity.Configuration
{
    public class RootConfiguration : IRootConfiguration
    {      
        public AdminConfiguration AdminConfiguration { get; } = new AdminConfiguration();
        public RegisterConfiguration RegisterConfiguration { get; } = new RegisterConfiguration();
    }
}








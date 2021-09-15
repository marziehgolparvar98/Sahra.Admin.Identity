using System;

namespace Sahra.DataLayer.Models.Identity
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}

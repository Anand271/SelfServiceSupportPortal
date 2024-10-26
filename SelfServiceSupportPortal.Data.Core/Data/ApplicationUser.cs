using Microsoft.AspNetCore.Identity;

namespace SelfServiceSupportPortal.Data.Core.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Company { get; set; }
    }
}

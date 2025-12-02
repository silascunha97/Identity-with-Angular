using Microsoft.AspNetCore.Identity;

namespace IdentityWithAngular.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
    }
}
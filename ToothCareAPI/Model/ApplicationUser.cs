using Microsoft.AspNetCore.Identity;

namespace ToothCareAPI.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}

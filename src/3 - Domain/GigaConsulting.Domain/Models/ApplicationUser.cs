using Microsoft.AspNetCore.Identity;

namespace GigaConsulting.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsDeleted { get; set; }
    }
}

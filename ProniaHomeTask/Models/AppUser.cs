using Microsoft.AspNetCore.Identity;

namespace ProniaHomeTask.Models
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;

namespace CharityCalculatorV2.Models.Domain
{
    // Class for the user (exists to conform to the open/closed principle)
    public class ApplicationUser: IdentityUser
    {
        public string UserId { get; set; }
    }
}

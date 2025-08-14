using Microsoft.AspNetCore.Identity;

namespace ST10090394_PROG7311_POE_Part2.Models
{
    public class Farmers
    {
        public int FarmerId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;

        public IdentityUser User { get; set; } = null!;

        // Initialize collection to avoid null issues
        public ICollection<Products> Products { get; set; } = new List<Products>();
    }
}

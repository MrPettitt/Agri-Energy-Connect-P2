using Microsoft.AspNetCore.Identity;

namespace ST10090394_PROG7311_POE_Part2.Models
{
    public class Employees
    {
        public int EmployeesId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;

        public IdentityUser User { get; set; } = null!;
    }
}

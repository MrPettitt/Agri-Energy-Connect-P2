using System.ComponentModel.DataAnnotations;

namespace ST10090394_PROG7311_POE_Part2.Models
{
    public class Products
    {
        public int ProductsId { get; set; }

        [Required]
        public int FarmerId { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string ProductCategory { get; set; } = string.Empty;

        [Required]
        public DateTime ProductionDate { get; set; }

        public Farmers Farmer { get; set; } = null!;
    }
}

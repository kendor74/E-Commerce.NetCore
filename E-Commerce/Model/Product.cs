using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace E_Commerce.Model
{
    public class Product
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(2000)]
        public string Description { get; set; } = string.Empty;

        public string CoverImage { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, double.MaxValue)]
        public decimal PriceAfterDiscount { get; set; }

        [Range(0, int.MaxValue)]
        public int Sold { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, 5)] // Assuming rating is between 0 and 5 stars
        public double Rating { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;


        public List<Image> Images { get; set; } = new List<Image>();

        public List<Color> Colors { get; set; } = new List<Color>();

        [Required]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public List<SubCategory> SubCategories { get; set; } = new List<SubCategory>();

        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}

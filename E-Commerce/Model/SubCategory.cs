using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Model
{
    public class SubCategory
    {
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string Image { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

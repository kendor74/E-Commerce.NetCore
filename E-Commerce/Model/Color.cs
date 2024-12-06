using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Model
{
    public class Color
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}

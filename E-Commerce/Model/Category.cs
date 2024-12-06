using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Model
{
    public class Category
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Image{ get; set; }
    }
}

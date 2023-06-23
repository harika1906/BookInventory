using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class BookStore
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public int ISBN { get; set; }
        [Required]
        public int QuantityAvailable { get; set; }
    }
}

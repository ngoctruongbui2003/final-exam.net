using System.ComponentModel.DataAnnotations;

namespace shoes_final_exam.Models
{
    public class Size
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public float SizeNumber { get; set; }

        // Relationship 1-n with Product
        public ICollection<Product>? Products { get; set; }
    }
}

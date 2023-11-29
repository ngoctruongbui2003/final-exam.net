using System.ComponentModel.DataAnnotations;

namespace shoes_final_exam.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tên hãng")]
        public string? Name { get; set; }
        // Relationship 1-n with Product
        public List<Product>? Products { get; set; }
    }
}

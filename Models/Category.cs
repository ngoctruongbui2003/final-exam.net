using System.ComponentModel.DataAnnotations;

namespace shoes_final_exam.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}

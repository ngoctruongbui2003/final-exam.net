using System.ComponentModel.DataAnnotations;

namespace shoes_final_exam.Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string? Image { get; set; }
		[Required]
		public string? Name { get; set; }
		public string? Description { get; set; }
		[Required]	
		public long Price { get; set; }
		[Required]
		public int Quantity { get; set; }
		[Required]
		public bool IsNew { get; set; }
		public DateTime CreatedDate { get; set; }

		// Relationship n-1 with Category
		public int CategoryId { get; set; }
		public Category? Category { get; set; }

        // Relationship n-1 with Size
        public int SizeId { get; set; }
        public Size? Size { get; set; }

        // Relationship 1-n with OrderDetail
        public ICollection<OrderDetail>? OrderDetails { get; set; }
	}
}

using System.ComponentModel.DataAnnotations;

namespace shoes_final_exam.Models
{
	public class OrderDetail
	{
		[Key]
		public int Id { get; set; }
		public long Price { get; set; }
		public int Quantity { get; set; }

		// Relationship n-1 with Product
		public int ProductId { get; set; }
		public Product? Product { get; set; }

		// Relationship n-1 with Order
		public int OrderId { get; set; }
		public Order? Order { get; set; }
	}
}

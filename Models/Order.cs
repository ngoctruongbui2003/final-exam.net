using System.ComponentModel.DataAnnotations;

namespace shoes_final_exam.Models
{
	public class Order
	{
		[Key]
		public int Id { get; set; }
		public long TotalPrice { get; set; }
		public int Quantity { get; set; }
		public int Status { get; set; }
		public DateTime CreatedDate { get; set; }

		// Relationship n-1 with User
		public AppUser? AppUser { get; set; }

		// Relationship 1-n with OrderDetail
		public ICollection<OrderDetail>? OrderDetails { get; set; }
	}
}

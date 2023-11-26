namespace shoes_final_exam.Models
{
	public class Cart
	{
		public List<CartItem> CartItems { get; set; }
		public decimal GrandTotal { get; set; }
	}
}

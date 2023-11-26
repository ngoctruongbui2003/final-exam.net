namespace shoes_final_exam.Models
{
	public class CartItem
	{
		public int ProductId { get; set; }
		public string? ProductImage { get; set; }
		public string? ProductName { get; set; }
		public float ProductSize { get; set; }
		public long ProductPrice { get; set; }
		public int Quantity { get; set; }
		public decimal Total
		{
			get { return ProductPrice * Quantity; }
		}

		public CartItem() { }
		public CartItem(Product product)
		{
			this.ProductId = product.Id;
			this.ProductImage = product.Image;
			this.ProductName = product.Name;
			this.ProductSize = product.Size!.SizeNumber;
			this.ProductPrice = product.Price;
			this.Quantity = 1;
		}
	}
}

namespace TH4_Nhom20.Models
{
    public class CartViewModel
    {
        public IEnumerable<CartModel> Carts { get; set; }
        public double Subtotal { get; set; }
    }
}

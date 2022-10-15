using Microsoft.Build.Framework;

namespace TH4_Nhom20.Models
{
    public class CartViewModel
    {
        public IEnumerable<CartModel> CartList { get; set; }
        public OrderModel Order { get; set; }
    }
}

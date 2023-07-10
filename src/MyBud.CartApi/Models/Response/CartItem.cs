namespace MyBud.CartApi.Models.Response
{
    public class CartItem
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public DateTime DateAdded { get; set; }
        public int Quantity { get; set; }
    }
}

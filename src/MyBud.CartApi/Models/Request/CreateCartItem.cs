namespace MyBud.CartApi.Models.Request
{
    public class CreateCartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

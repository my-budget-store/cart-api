using System.ComponentModel.DataAnnotations;

namespace MyBud.CartApi.Models.Core
{
    public class CartEntity
    {
        [Key]
        public int CartId { get; set; }
        public Guid UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime DateAdded { get; set; }
        public int Quantity { get; set; }
    }
}

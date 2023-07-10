using MyBud.CartApi.Models.Core;

namespace MyBud.CartApi.Interfaces
{
    public interface ICartRepository
    {
        Task<CartEntity?> GetCartItemById(int CartId);
        Task<IQueryable<CartEntity>> GetCartItems(Guid _userId);
        Task<CartEntity> CreateOrUpdateCart(CartEntity Cart);
        //Task<IEnumerable<CartEntity>> SearchCarts(string value);
        //Task<CartEntity> UpdateCart(CartEntity Cart);
        //Task<bool> DeleteCart(CartEntity Cart);
    }
}

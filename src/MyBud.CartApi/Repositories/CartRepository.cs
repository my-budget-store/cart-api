using MyBud.CartApi.Interfaces;
using MyBud.CartApi.Models.Core;
using System.Security.Claims;

namespace MyBud.CartApi.Repositories
{
    //TODO: modify "template" variable names as per your use case
    public class CartRepository : ICartRepository
    {
        private readonly CartContext _context;

        public CartRepository(CartContext context)
        {
            _context = context;
        }

        public async Task<CartEntity?> GetCartItemById(int templateId)
        {
            var template = await _context.Carts.FindAsync(templateId);

            return template;
        }

        public Task<IQueryable<CartEntity>> GetCartItems(Guid userId)
        {
            var carts = _context.Carts.Where(c=>c.UserId==userId);

            return Task.FromResult(carts);
        }

        public async Task<CartEntity> CreateOrUpdateCart(CartEntity cartItem)
        {
            var existingCartItem = _context.Carts.FirstOrDefault(c => c.UserId == cartItem.UserId && c.ProductId == cartItem.ProductId);
            if (existingCartItem!=null)
            {
                cartItem.Quantity = existingCartItem.Quantity + 1;
                _context.Update(existingCartItem);
            }

            await _context.AddAsync(cartItem);
            _context.SaveChanges();

            return cartItem;
        }

        //public Task<IEnumerable<CartEntity>> SearchCarts(string value)
        //{
        //    var templates = _context.Carts.Where(p => p.Name.Contains(value)).AsEnumerable();

        //    return Task.FromResult(templates);
        //}

        //public async Task<CartEntity> UpdateCart(CartEntity template)
        //{
        //    var existingCart = await _context.Carts.FindAsync(template.CartId);

        //    if (existingCart != null)
        //        _context.Update(existingCart);

        //    await _context.SaveChangesAsync();

        //    return existingCart;
        //}

        //public async Task<CartEntity> UpdateCartPrice(CartEntity template)
        //{
        //    var existingCart = await _context.Carts.FindAsync(template.CartId);

        //    if (existingCart != null)
        //    {
        //        //ToDo: Implement Logic here
        //        _context.Update(existingCart);
        //    }

        //    await _context.SaveChangesAsync();

        //    return existingCart;
        //}

        //public async Task<bool> DeleteCart(CartEntity template)
        //{
        //    var templatestate = _context.Remove(template);
        //    await _context.SaveChangesAsync();

        //    if (templatestate.State.Equals(Microsoft.EntityFrameworkCore.EntityState.Deleted))
        //    {
        //        return true;
        //    }

        //    return false;
        //}
    }
}

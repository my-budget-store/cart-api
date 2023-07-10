using Microsoft.EntityFrameworkCore;
using MyBud.CartApi.Models.Core;

namespace MyBud.CartApi.Repositories
{
    public class CartContext : DbContext
    {
        public CartContext(DbContextOptions<CartContext> options) : base(options)
        {
        }

        public DbSet<CartEntity> Carts { get; set; }
    }
}

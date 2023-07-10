using Microsoft.EntityFrameworkCore;
using MyBud.CartApi.Repositories;

namespace MyBud.CartApi.HostingExtensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, ConfigurationManager configuration)
        {
            return services.AddDbContext<CartContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("CartContext")));
        }

        public static void UseConfiguredDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<CartContext>();
                db.Database.Migrate();
            }
            //app.UseMiddleware<DatabaseProvider>();
        }

        private class DatabaseProvider
        {
            private readonly RequestDelegate _next;
            private readonly CartContext _dbContext;

            public DatabaseProvider(RequestDelegate next, CartContext dbContext)
            {
                _next = next;
                _dbContext = dbContext;
            }

            public async Task Invoke(HttpContext httpContext)
            {
                await _dbContext.Database.MigrateAsync();
                await _next.Invoke(httpContext);
            }
        }
    }
}

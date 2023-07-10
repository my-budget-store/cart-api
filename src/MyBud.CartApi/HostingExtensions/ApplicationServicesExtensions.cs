using MyBud.CartApi.Interfaces;
using MyBud.CartApi.Repositories;

namespace MyBud.CartApi.HostingExtensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            return services
                .AddHttpContextAccessor()
                .AddTransient<ICartRepository, CartRepository>();
        }
    }
}

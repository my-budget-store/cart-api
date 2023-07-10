using System.Text.Json.Serialization;

namespace MyBud.CartApi.HostingExtensions
{
    public static class MvcExtensions
    {
        public static IServiceCollection ConfigureMvc(this IServiceCollection services)
        {
            services
                .AddControllers()
                .AddJsonOptions(opts =>
                {
                    var enumConverter = new JsonStringEnumConverter();
                    opts.JsonSerializerOptions.Converters.Add(enumConverter);
                });

            return services;
        }
    }
}

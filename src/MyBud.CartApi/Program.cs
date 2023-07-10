using MyBud.CartApi.HostingExtensions;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services
    .ConfigureDatabase(config)
    .ConfigureApplicationServices()
    .ConfigureApiVersioningAndExplorer()
    .ConfigureAuth(config)
    .ConfigureCors(config)
    .ConfigureSwagger()
    .AddMvc();

var app = builder.Build();

app.UseConfiguredDatabase();
app.UseConfiguredCors();
app.UseConfiguredContentNegotiation();
//app.UseResponseCompression();
app.UseConfiguredCorrelationContext();
if (app.Environment.IsDevelopment())
    app.UseConfiguredSwagger();
app.UseAuthentication()
   .UseAuthorization();
app.MapControllers();
app.Run();
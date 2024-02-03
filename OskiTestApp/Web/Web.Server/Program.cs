using Web.Server.Repositories;
using Web.Server.Repositories.Interfaces;
using Web.Server.Services;
using Web.Server.Services.Interfaces;

public class Program
{
    public static void Main(string[] args)
    {
        var configuration = GetConfiguration();

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddTransient<ITestService, TestService>();
        builder.Services.AddTransient<IUserTestRepository, UserTestRepository>();
        builder.Services.AddTransient<IUserService, UserService>();
        builder.Services.AddTransient<IUserRepository, UserRepository>();
        builder.Services.AddTransient<ITestRepository, TestRepository>();

        builder.Services.AddControllers();
        builder.Services.AddOptions();
        builder.Services.AddMemoryCache();
        var app = builder.Build();

        app.UseSwagger()
            .UseSwaggerUI(setup =>
            {
                setup.SwaggerEndpoint($"{configuration["PathBase"]}/swagger/v1/swagger.json", "Web.Server.API V1");
            });

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
            endpoints.MapControllers();
        });

        app.Run();

        IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}

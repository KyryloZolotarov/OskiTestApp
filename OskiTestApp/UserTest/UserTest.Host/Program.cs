using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using UserTest.Host.Data;
using UserTest.Host.Repositories;
using UserTest.Host.Repositories.Interfaces;
using UserTest.Host.Services;
using UserTest.Host.Services.Interfaces;

public class Program
{
    public static void Main(string[] args)
    {
        var configuration = GetConfiguration();

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAutoMapper(typeof(Program));

        builder.Services.AddTransient<IUserTestService, UserTestService>();
        builder.Services.AddTransient<IUserTestRepository, UserTestRepository>();

        builder.Services.AddDbContextFactory<ApplicationDbContext>(opts =>
            opts.UseSqlServer(configuration["ConnectionString"]));
        builder.Services.AddScoped<IDbContextWrapper<ApplicationDbContext>, DbContextWrapper<ApplicationDbContext>>();
        builder.Services.AddControllers();
        builder.Services.AddOptions();
        builder.Services.AddMemoryCache();
        var app = builder.Build();

        // app.UseSwagger()
        //     .UseSwaggerUI(setup =>
        //     {
        //         setup.SwaggerEndpoint($"{configuration["PathBase"]}/swagger/v1/swagger.json", "UserTest.API V1");
        //     });

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
            endpoints.MapControllers();
        });

        CreateDbIfNotExists(app);
        app.Run();

        IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables();

            return builder.Build();
        }

        void CreateDbIfNotExists(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();

                DbInitializer.Initialize(context).Wait();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred creating the DB.");
            }
        }
    }
}
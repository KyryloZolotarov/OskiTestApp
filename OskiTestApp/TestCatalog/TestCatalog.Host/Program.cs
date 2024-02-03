using Infrastructure.Services.Interfaces;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using TestCatalog.Host.Data;
using Microsoft.AspNetCore.Cors.Infrastructure;
using TestCatalog.Host.Repositories.Interfaces;
using TestCatalog.Host.Services;
using TestCatalog.Host.Services.Interfaces;
using TestCatalog.Host.Repositories;

public class Program
{
    public static void Main(string[] args)
    {
        var configuration = GetConfiguration();

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAutoMapper(typeof(Program));

        builder.Services.AddTransient<ITestService, TestService>();
        builder.Services.AddTransient<ITestRepository, TestRepository>();
        builder.Services.AddTransient<IQuestionService, QuestionService>();
        builder.Services.AddTransient<IQuestionRepository, QuestionRepository>();

        builder.Services.AddDbContextFactory<ApplicationDbContext>(opts =>
            opts.UseSqlServer(configuration["ConnectionString"]));
        builder.Services.AddScoped<IDbContextWrapper<ApplicationDbContext>, DbContextWrapper<ApplicationDbContext>>();
        builder.Services.AddControllers();
        builder.Services.AddOptions();
        builder.Services.AddMemoryCache();
        var app = builder.Build();

        app.UseSwagger()
            .UseSwaggerUI(setup =>
            {
                setup.SwaggerEndpoint($"{configuration["PathBase"]}/swagger/v1/swagger.json", "TestCatalog.API V1");
            });

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
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Web.Server.Repositories;
using Web.Server.Repositories.Interfaces;
using Web.Server.Services;
using Web.Server.Services.Interfaces;

namespace Web.Server;

public class Program
{
    public static void Main(string[] args)
    {
        var configuration = GetConfiguration();
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.Configure<AppSettings>(configuration);
        builder.Services.AddAutoMapper(typeof(Program));
        builder.Services.AddTransient<ITestService, TestService>();
        builder.Services.AddTransient<IUserTestRepository, UserTestRepository>();
        builder.Services.AddTransient<IUserService, UserService>();
        builder.Services.AddTransient<IUserRepository, UserRepository>();
        builder.Services.AddTransient<ITestRepository, TestRepository>();
        builder.Services.AddHttpClient();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<IHttpClientService, HttpClientService>();
        builder.Services.AddControllers();
        builder.Services.AddOptions();
        builder.Services.AddMemoryCache();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder.WithOrigins("https://localhost:5173") // React app address
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
        });
        builder.Services.AddAuthentication("CookieAuth")
            .AddCookie("CookieAuth", options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.Name = "YourAuthCookieName";
                options.Cookie.HttpOnly = false;
                // options.ExpireTimeSpan = ...
                // options.LoginPath = ...
                // options.LogoutPath = ...
            });
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("CookieAuth", policy =>
            {
                policy.AuthenticationSchemes.Add("CookieAuth");
                policy.RequireAuthenticatedUser();
                // Дополнительные требования
            });
        });
        var app = builder.Build();

        // app.UseSwagger()
        //     .UseSwaggerUI(setup =>
        //     {
        //         setup.SwaggerEndpoint($"{configuration["PathBase"]}/swagger/v1/swagger.json", "Web.Server.API V1");
        //     });

        app.UseRouting();
        app.UseCors("CorsPolicy");
        app.UseAuthentication();
        app.UseAuthorization();
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
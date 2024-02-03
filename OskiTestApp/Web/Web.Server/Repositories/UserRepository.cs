using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Options;
using Web.Server.Repositories.Interfaces;

namespace Web.Server.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IHttpClientService _httpClient;
        private readonly IOptions<AppSettings> _settings;

        public UserRepository(IHttpClientService httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;
        }
    }
}

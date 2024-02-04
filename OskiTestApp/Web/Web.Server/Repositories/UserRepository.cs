using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Options;
using Web.Server.Models;
using Web.Server.Models.Dtos;
using Web.Server.Models.Requests;
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

        public async Task<UserDto> LoginAsync(LoginDto login)
        {
            return await _httpClient.SendAsync<UserDto, LoginDto>($"{_settings.Value.UserProfilesUrl}/Login", HttpMethod.Post, login);
        }

        public async Task<UserDto> SignUpAsync(AddUserRequest user)
        {
            return await _httpClient.SendAsync<UserDto, AddUserRequest>($"{_settings.Value.UserProfilesUrl}/AddUser", HttpMethod.Post, user);
        }
    }
}

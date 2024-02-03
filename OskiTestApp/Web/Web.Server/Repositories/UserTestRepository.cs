using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Options;
using Web.Server.Models.Dtos;
using Web.Server.Repositories.Interfaces;
using Web.Server.ViewModels;

namespace Web.Server.Repositories
{
    public class UserTestRepository : IUserTestRepository
    {
        private readonly IHttpClientService _httpClient;
        private readonly IOptions<AppSettings> _settings;

        public UserTestRepository(IHttpClientService httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;
        }

        public async Task<IEnumerable<UserTestDto>> GetAvailableTestsAsync(string userId)
        {
            return await _httpClient.SendAsync<IEnumerable<UserTestDto>>($"{_settings.Value.UserTestUrl}/GetUserTests?userId={userId}&isTestComleted={false}", HttpMethod.Get);
        }

        public async Task<IEnumerable<UserTestDto>> GetPassedTestsAsync(string userId)
        {
            return await _httpClient.SendAsync<IEnumerable<UserTestDto>>($"{_settings.Value.UserTestUrl}/GetUserTests?userId={userId}&isTestComleted={true}", HttpMethod.Get);
        }
    }
}

using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Options;
using Web.Server.Models.Dtos;
using Web.Server.Models.Requests;
using Web.Server.Repositories.Interfaces;

namespace Web.Server.Repositories;

public class TestRepository : ITestRepository
{
    private readonly IHttpClientService _httpClient;
    private readonly IOptions<AppSettings> _settings;

    public TestRepository(IHttpClientService httpClient, IOptions<AppSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings;
    }

    public async Task<TestDto> GetSelectedTestAsync(int testId)
    {
        return await _httpClient.SendAsync<TestDto>($"{_settings.Value.TestCatalogUrl}/test/GetTest/{testId}",
            HttpMethod.Get);
    }

    public async Task<TestsNamesDto> GetTestNamesAsync(TestsNamesRequest testsIds)
    {
        return await _httpClient.SendAsync<TestsNamesDto, TestsNamesRequest>(
            $"{_settings.Value.TestCatalogUrl}/test/GetTestsNames/", HttpMethod.Post, testsIds);
    }
}
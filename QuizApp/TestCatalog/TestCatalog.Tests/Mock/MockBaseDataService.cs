using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace TestCatalog.Tests.Mock;

public class MockBaseDataService : BaseDataService<MockDbContext>
{
    public MockBaseDataService(
        IDbContextWrapper<MockDbContext> dbContextWrapper,
        ILogger<MockBaseDataService> logger)
        : base(dbContextWrapper, logger)
    {
    }

    public async Task RunWithException()
    {
        await ExecuteSafeAsync<bool>(() => throw new Exception());
    }

    public async Task RunWithoutException()
    {
        await ExecuteSafeAsync(() => Task.FromResult(true));
    }
}
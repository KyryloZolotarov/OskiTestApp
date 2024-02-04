using Infrastructure.Services.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCatalog.Tests.Mock
{
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
}

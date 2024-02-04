using Infrastructure.Services.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitTests.Mocks
{
    internal class MockService : BaseDataService<MockDbContext>
    {
        public MockService(
            IDbContextWrapper<MockDbContext> dbContextWrapper,
            ILogger<MockService> logger)
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

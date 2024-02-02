using AutoMapper;
using Infrastructure.Exceptions;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using TestCatalog.Host.Data;
using TestCatalog.Host.Data.Entities;
using TestCatalog.Host.Models.Dtos;
using TestCatalog.Host.Models.Requests;
using TestCatalog.Host.Repositories;
using TestCatalog.Host.Repositories.Interfaces;
using TestCatalog.Host.Services.Interfaces;

namespace TestCatalog.Host.Services
{
    public class TestService : BaseDataService<ApplicationDbContext>, ITestService
    {
        private readonly ITestRepository _testManageRepository;
        private readonly IMapper _mapper;

        public TestService(ITestRepository testManageRepository,
            IMapper mapper,
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger)
            : base(dbContextWrapper, logger)
        {
            _mapper = mapper;
            _testManageRepository = testManageRepository;
        }
        public async Task AddTestAsync(AddTestRequest test)
        {
            await ExecuteSafeAsync(async () =>
            {
                var testAdd = new TestEntity()
                {
                    Description = test.Description,
                    Name = test.Name,
                };
                await _testManageRepository.AddTestAsync(testAdd);
            });
        }

        public async Task DeleteTestAsync(int testId)
        {
            var testExists = await ExecuteSafeAsync(async () => await _testManageRepository.GetTestAsync(testId));

            if (testExists == null)
            {
                throw new BusinessException($"Test with id: {testId} not found");
            }

            await ExecuteSafeAsync(async () =>
            {
                await _testManageRepository.DeleteTestAsync(testExists);
            });
        }

        public async Task<TestDto> GetTestAsync(int testId)
        {

            return await ExecuteSafeAsync(async () =>
            {
                var result = await _testManageRepository.GetTestAsync(testId);
                var mappedResult = _mapper.Map<TestDto>(result);
                return mappedResult;
            });
        }

        public async Task UpdateTestAsync(UpdateTestRequest test)
        {
            var testExists = await ExecuteSafeAsync(async () => await _testManageRepository.GetTestAsync(test.Id));

            if (testExists == null)
            {
                throw new BusinessException($"Test with id: {test.Id} not found");
            }

            if (test.Name != null)
            {
                testExists.Name = test.Name;
            }

            if (test.Description != null)
            {
                testExists.Description = test.Description;
            }

            await ExecuteSafeAsync(async () =>
            {
                await _testManageRepository.UpdateTestAsync(testExists);
            });
        }
    }
}

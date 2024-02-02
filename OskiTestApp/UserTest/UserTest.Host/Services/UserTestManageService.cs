using Infrastructure.Exceptions;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using UserTest.Host.Data;
using UserTest.Host.Data.Entities;
using UserTest.Host.Models.Dtos;
using UserTest.Host.Models.Requests;
using UserTest.Host.Repositories.Interfaces;
using UserTest.Host.Services.Interfaces;

namespace UserTest.Host.Services
{
    public class UserTestManageService : BaseDataService<ApplicationDbContext>, IUserTestManageService
    {
        private readonly IUserTestManageRepository _userTestManageRepository;
        public UserTestManageService(IUserTestManageRepository userTestManageRepository,
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger)
            : base(dbContextWrapper, logger)
        {
            _userTestManageRepository = userTestManageRepository;
        }

        public async Task AddUserTestAsync(AddUserTestRequest userTest)
        {
            await ExecuteSafeAsync(async () =>
            {
                var userTestAdd = new UserTestEntity()
                {
                    UserId = userTest.UserId,
                    TestId = userTest.TestId,
                    Mark = userTest.Mark,
                };

                await _userTestManageRepository.AddUserTestAsync(userTestAdd);
            });
        }

        public async Task DeleteUserTestAsync(int userTestId)
        {
            var userTestExists = await ExecuteSafeAsync(async () => await _userTestManageRepository.GetUserTestAsync(userTestId));

            if (userTestExists == null)
            {
                throw new BusinessException($"UserTest with id: {userTestId} not found");
            }

            await ExecuteSafeAsync(async () =>
            {
                await _userTestManageRepository.DeleteUserTestAsync(userTestExists);
            });
        }

        public async Task UpdateUserTestAsync(UpdateUserTestRequest userTest)
        {
            var userTestExists = await ExecuteSafeAsync(async () => await _userTestManageRepository.GetUserTestAsync(userTest.Id));

            if (userTestExists == null)
            {
                throw new BusinessException($"UserTest with id: {userTest.Id} not found");
            }

            if (userTest.TestId != null)
            {
                userTestExists.TestId = userTest.TestId;
            }

            if (userTest.UserId != null)
            {
                userTestExists.UserId = userTest.UserId;
            }

            if (userTest.Mark != null)
            {
                userTestExists.Mark = userTest.Mark;
            }

            await ExecuteSafeAsync(async () =>
            {
                await _userTestManageRepository.UpdateUserTestAsync(userTestExists);
            });
        }
    }
}

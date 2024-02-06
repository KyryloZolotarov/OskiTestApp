using AutoMapper;
using Infrastructure.Exceptions;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using UserTest.Host.Data;
using UserTest.Host.Data.Entities;
using UserTest.Host.Models.Dtos;
using UserTest.Host.Models.Requests;
using UserTest.Host.Repositories.Interfaces;
using UserTest.Host.Services.Interfaces;

namespace UserTest.Host.Services;

public class UserTestService : BaseDataService<ApplicationDbContext>, IUserTestService
{
    private readonly IMapper _mapper;
    private readonly IUserTestRepository _userTestRepository;

    public UserTestService(IUserTestRepository userTestRepository,
        IMapper mapper,
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger)
        : base(dbContextWrapper, logger)
    {
        _mapper = mapper;
        _userTestRepository = userTestRepository;
    }

    public async Task AddUserTestAsync(AddUserTestRequest userTest)
    {
        await ExecuteSafeAsync(async () =>
        {
            var userTestAdd = new UserTestEntity
            {
                UserId = userTest.UserId,
                TestId = userTest.TestId,
                IsTestCompleted = userTest.IsTestCompleted,
                Mark = userTest.Mark
            };

            await _userTestRepository.AddUserTestAsync(userTestAdd);
        });
    }

    public async Task DeleteUserTestAsync(string userId, int testId)
    {
        var userTestExists =
            await ExecuteSafeAsync(async () => await _userTestRepository.GetUserTestAsync(userId, testId));

        if (userTestExists == null)
            throw new BusinessException($"User Test with user id {userId} and testId {testId} not found");

        await ExecuteSafeAsync(async () => { await _userTestRepository.DeleteUserTestAsync(userTestExists); });
    }

    public async Task<IEnumerable<UserTestDto>> GetUserTestsAsync(string userId, bool isTestComleted)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _userTestRepository.GetUserTestsAsync(userId, isTestComleted);
            var mappedResult = result.Select(s => _mapper.Map<UserTestDto>(s)).ToList();
            return mappedResult;
        });
    }

    public async Task UpdateUserTestAsync(UpdateUserTestRequest userTest)
    {
        var userTestExists = await ExecuteSafeAsync(async () =>
            await _userTestRepository.GetUserTestAsync(userTest.UserId, userTest.TestId));

        if (userTestExists == null)
            throw new BusinessException(
                $"User Test with user id {userTest.UserId} and testId {userTest.TestId} not found");

        if (userTest.TestId != null) userTestExists.TestId = userTest.TestId;

        if (userTest.UserId != null) userTestExists.UserId = userTest.UserId;

        if (userTest.IsTestCompleted != null) userTestExists.IsTestCompleted = userTest.IsTestCompleted;

        if (userTest.Mark != null) userTestExists.Mark = userTest.Mark;

        await ExecuteSafeAsync(async () => { await _userTestRepository.UpdateUserTestAsync(userTestExists); });
    }
}
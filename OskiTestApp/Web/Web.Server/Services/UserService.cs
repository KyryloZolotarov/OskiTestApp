using AutoMapper;
using Web.Server.Models;
using Web.Server.Models.Requests;
using Web.Server.Repositories.Interfaces;
using Web.Server.Services.Interfaces;
using Web.Server.ViewModels;

namespace Web.Server.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserTestRepository _userTestRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository,
            IMapper mapper,
            IUserTestRepository userTestRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _userTestRepository = userTestRepository;
        }

        public async Task<UserViewModel> LoginAsync(LoginViewModel login)
        {
            var loginToDb = _mapper.Map<LoginDto>(login);
            var result = await _userRepository.LoginAsync(loginToDb);
            var user = _mapper.Map<UserViewModel>(result);
            return user;
        }

        public async Task<UserViewModel> SignUpAsync(AddUserRequest user)
        {
            var result = await _userRepository.SignUpAsync(user);
            var resultUser = _mapper.Map<UserViewModel>(result);
            return resultUser;
        }
    }
}

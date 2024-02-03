using Web.Server.Repositories.Interfaces;
using Web.Server.Services.Interfaces;

namespace Web.Server.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserTestRepository _userTestRepository;

        public UserService(IUserRepository userRepository,
            IUserTestRepository userTestRepository)
        {
            _userRepository = userRepository;
            _userTestRepository = userTestRepository;
        }
    }
}

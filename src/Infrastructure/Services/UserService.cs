using Microsoft.Extensions.Configuration;
using Woolworth.Application.Common.Interfaces;
using Woolworth.Application.User.Queries;
using Woolworth.Infrastructure.Models;

namespace Woolworth.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly UserOptions _userOptions;
        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
            //Initialize use options once as they are from Config
            //Not expected to change for the lifetime of the object
            _userOptions = GetUserOptions();
        }

        private UserOptions GetUserOptions()
        {
            return _configuration
                .GetSection(UserOptions.User)
                .Get<UserOptions>();
        }

        public UserDto GetCurrentUser()
        {
            return new UserDto(_userOptions.Name, _userOptions.Token);
        }
    }
}

using efcore2_webapi.AppServices.Contracts;
using efcore2_webapi.Domain.Entities;
using efcore2_webapi.Infrastructure;
using efcore2_webapi.Repository;
using efcore2_webapi.Models;

namespace efcore2_webapi.AppServices
{
    public class UserAppService : IUserAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UserAppService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public int Save(UserDto userDto)
        {
            int userId = 0;
            // using (_unitOfWork)
            {
                var user = new User(userDto.Name);
                user.Email = userDto.Email;
                user.Username = userDto.Username;

                _userRepository.Save(user);

                _unitOfWork.Commit();
            }

            return userId;
        }
    }
}
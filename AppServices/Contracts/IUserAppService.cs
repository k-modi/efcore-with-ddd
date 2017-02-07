using efcore2_webapi.Models;

namespace efcore2_webapi.AppServices.Contracts
{
    public interface IUserAppService
    {
        int Save(UserDto userDto);
    }
}
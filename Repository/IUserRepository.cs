using System.Collections.Generic;
using efcore2_webapi.Repository.Infrastructure;
using efcore2_webapi.Domain.Entities;

namespace efcore2_webapi.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        IList<User> GetAllUsers();
        
        User FindByUsername(string username);
        
        int TotalUserCount();
    }
}

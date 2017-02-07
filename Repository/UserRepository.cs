using System.Collections.Generic;
using System.Linq;
using efcore2_webapi.Domain.Entities;
using efcore2_webapi.Repository.Infrastructure;

namespace efcore2_webapi.Repository
{
    internal class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IDbContextManager contextMgr) : base(contextMgr)
        { }

        public User FindByUsername(string username)
        {
            return RootSet.SingleOrDefault(p => p.Username == username);
        }

        public IList<User> GetAllUsers()
        {
            return RootSet.ToList();
        }

        public int TotalUserCount()
        {
            return RootSet.Count();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using efcore2_webapi.Repository.Mappings;

namespace efcore2_webapi.Repository.Infrastructure
{
    internal class DbContextManager : IDbContextManager
    {

        private readonly DbContext _dbContext;

        public DbContextManager(ModelBuilderContext options)
        {
            _dbContext = options;
        }

        public DbContext GetDbContext()
        {
            return _dbContext;
        }
    }
}
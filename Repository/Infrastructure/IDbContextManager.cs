using Microsoft.EntityFrameworkCore;

namespace efcore2_webapi.Repository.Infrastructure
{
    public interface IDbContextManager
    {
        DbContext GetDbContext();
    }
}
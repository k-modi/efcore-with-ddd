using Microsoft.EntityFrameworkCore;

namespace efcore2_webapi.Repository.Mappings
{
    internal class ModelBuilderContext : DbContext
    {
        public ModelBuilderContext(DbContextOptions options) : base(options)
        { }
    }
}
using efcore2_webapi.Infrastructure.DomainKernel;
using Microsoft.EntityFrameworkCore;

namespace efcore2_webapi.Repository.Infrastructure
{
    public abstract class Repository<TRoot> : IRepository<TRoot>
        where TRoot : AggregateRoot
    {
        protected DbSet<TRoot> RootSet { get; set; }

        private readonly DbContext _context;

        protected Repository(IDbContextManager contextMgr)
        {
            _context = contextMgr.GetDbContext();
            RootSet = _context.Set<TRoot>();
        }

        //TODO: Get the DbContext (aka NH Session from ContextManager (aka NH SessionManager))

        public virtual void Delete(TRoot aggregateRoot)
        {
            RootSet.Remove(aggregateRoot);
        }

        public virtual TRoot FindById(int id)
        {
            return RootSet.Find(id);
        }

        public virtual TRoot Save(TRoot aggregateRoot)
        {
            RootSet.Add(aggregateRoot);
            _context.SaveChanges();
            return aggregateRoot;
        }

        public virtual TRoot Update(TRoot aggregateRoot)
        {
            RootSet.Update(aggregateRoot);
            _context.SaveChanges();
            return aggregateRoot;
        }
    }
}

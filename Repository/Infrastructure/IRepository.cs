using efcore2_webapi.Infrastructure.DomainKernel;

namespace efcore2_webapi.Repository.Infrastructure
{
    public interface IRepository<TRoot> 
        where TRoot : AggregateRoot
    {
        TRoot FindById(int id);

        TRoot Save(TRoot aggregateRoot);

        TRoot Update(TRoot aggregateRoot);

        void Delete(TRoot aggregateRoot);
    }
}

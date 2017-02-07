namespace efcore2_webapi.Infrastructure.DomainKernel
{
    public abstract class Entity
    {
        public virtual int Id { get; protected internal set; }

        public virtual bool IsTransient()
        {
            return Id <= 0;
        }
    }
}

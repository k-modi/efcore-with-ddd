using System;

namespace efcore2_webapi.Infrastructure.DomainKernel
{
    public abstract class Identity<T> : IEquatable<Identity<T>>, IIdentity
        where T : AggregateRoot
    {
        protected Identity(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public bool Equals(Identity<T> id)
        {
            if (ReferenceEquals(this, id)) return true;
            if (ReferenceEquals(null, id)) return false;

            return Id.Equals(id.Id);
        }

        public override bool Equals(object anotherObject)
        {
            return Equals(anotherObject as Identity<T>);
        }

        public override int GetHashCode()
        {
            return (typeof(T).GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{typeof(T).Name} [Id={Id}]";
        }
    }
}
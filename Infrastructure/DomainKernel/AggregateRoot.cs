using System;
using System.Collections.Generic;
using System.Linq;

namespace efcore2_webapi.Infrastructure.DomainKernel
{
    public abstract class AggregateRoot : Entity
    {
        #region Domain Events

        readonly IList<IDomainEvent> _domainEvents = new List<IDomainEvent>();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="domainEvent"></param>
		/// <param name="allowTransient">If false do not allow the event if the aggregate root is not yet a persistent entity in database</param>
		/// <param name="allowDuplicates">If false do not allow events of same object instance to be raised</param>
		/// <param name="allowSimilar">If false do not allow events of same event object type to be raised</param>
		protected internal virtual void RaiseEvent(IDomainEvent domainEvent, bool allowTransient = false, bool allowDuplicates = false, bool allowSimilar = true)
		{
			if (!allowDuplicates && _domainEvents.Contains(domainEvent))
				return;

			if (!allowTransient && this.IsTransient())
				return;

			if (!allowSimilar && this.FindEvents(domainEvent.GetType()).Any())
			{
				return;
			}

			_domainEvents.Add(domainEvent);
		}

		/// <summary>
		/// Usefull when you need to collect your domain event multiple property values which are changed from different domain events
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
        protected virtual IDomainEvent[] FindEvents(Type type)
		{
			return _domainEvents.Where(de => de.GetType() == type).ToArray();
		}

		#endregion Domain Events
	}
}

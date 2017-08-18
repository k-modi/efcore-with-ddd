using System;
namespace efcore2_webapi.Infrastructure.DomainKernel
{
	public interface IDomainEvent
    {
		DateTime Timestamp { get; }
	}
}

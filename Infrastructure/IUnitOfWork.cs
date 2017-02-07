using System;

namespace efcore2_webapi.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
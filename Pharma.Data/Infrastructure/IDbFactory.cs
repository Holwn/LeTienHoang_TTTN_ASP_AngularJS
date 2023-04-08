using System;

namespace Pharma.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        PharmaDbContext Init();
    }
}
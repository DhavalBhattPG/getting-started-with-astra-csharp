using System.Collections.Generic;
using Cassandra;

namespace getting_started_with_apollo_csharp.Interfaces
{
    public interface IDataStaxService
    {
        ISession Session { get; }
    }
}
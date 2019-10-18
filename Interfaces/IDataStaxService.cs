using System.Collections.Generic;
using Cassandra;

namespace getting_started_with_apollo_csharp.Interfaces
{
    public interface IDataStaxService
    {
        ISession Session { get; }

        System.Tuple<bool, string> SaveConnection(string username, string password, string keyspace, string secureConnectBundlePath);
        System.Tuple<bool, string> TestConnection(string username, string password, string keyspace, string secureConnectBundlePath);
    }
}
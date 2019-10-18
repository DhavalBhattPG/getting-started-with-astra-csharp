using System;
using Cassandra;
using getting_started_with_apollo_csharp.Models;

namespace getting_started_with_apollo_csharp.Services
{
    public class ApolloService : Interfaces.IDataStaxService
    {
        private static readonly ApolloService _ApolloServiceInstance = new ApolloService();
        private ISession _session;

        public ISession Session
        {
            get
            {
                if (_session==null)
                {
                    _session =  ConnectToApollo(Environment.GetEnvironmentVariable("ApolloUsername"),
                    Environment.GetEnvironmentVariable("ApolloPassword"),
                    Environment.GetEnvironmentVariable("ApolloKeyspace"),
                    Environment.GetEnvironmentVariable("SecureConnectBundlePath"));

                    _session.UserDefinedTypes.Define(UdtMap.For<location_udt>());
                } 
                    return _session;
            }
        }

        private ISession ConnectToApollo(string username, string password, string keyspace, string secureConnectBundlePath) {
            var session =  Cluster.Builder()
                       .WithCloudSecureConnectionBundle(secureConnectBundlePath)
                       .WithCredentials(username, password)
                       .Build()
                       .Connect(keyspace);   
                    
            return session;
        }

        public Tuple<bool, string> SaveConnection(string username, string password, string keyspace, string secureConnectBundlePath)
        {
            try{
                var session = ConnectToApollo(username, password, keyspace, secureConnectBundlePath);
                Environment.SetEnvironmentVariable("ApolloUsername", username);
                Environment.SetEnvironmentVariable("ApolloPassword", password);
                Environment.SetEnvironmentVariable("ApolloKeyspace", keyspace);
                Environment.SetEnvironmentVariable("SecureConnectBundlePath", secureConnectBundlePath);
                _session = session;
                _session.UserDefinedTypes.Define(UdtMap.For<location_udt>());
                return new Tuple<bool, string>(true, null);
             } catch (Exception ex)
            {
                return new Tuple<bool, string>(false, ex.Message);
            }
        }

        public Tuple<bool, string> TestConnection(string username, string password, string keyspace, string secureConnectBundlePath)
        {
            try{
                var session = ConnectToApollo(username, password, keyspace, secureConnectBundlePath);
                return new Tuple<bool, string>(true, null);
             } catch (Exception ex)
            {
                return new Tuple<bool, string>(false, ex.Message);
            }
        }
    }
}
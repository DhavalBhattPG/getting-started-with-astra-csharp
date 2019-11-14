using System;
using Cassandra;
using System.Threading;
using getting_started_with_apollo_csharp.Models;
using System.Threading.Tasks;

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
                //If the session is null then create a new one
                if (_session == null)
                {
                    //Create the new Apollo session then save the parameters to Environment Variables for later use in the same session
                    _session = ConnectToApollo(Environment.GetEnvironmentVariable("ApolloUsername"),
                    Environment.GetEnvironmentVariable("ApolloPassword"),
                    Environment.GetEnvironmentVariable("ApolloKeyspace"),
                    Environment.GetEnvironmentVariable("SecureConnectBundlePath")).Result;
                }
                return _session;
            }
        }

        /// <summary>
        /// Creates a connection to Apollo with the specified parameters
        /// </summary>
        /// <param name="username">The Apollo user name</param>
        /// <param name="password">The Apollo password</param>
        /// <param name="keyspace">The keyspace in Apollo</param>
        /// <param name="secureConnectBundlePath">The local file path were the secure connect bundle was saved</param>
        /// <returns>The connected session object</returns>
        private async Task<ISession> ConnectToApollo(string username, string password, string keyspace, string secureConnectBundlePath)
        {
            var session = await Cluster.Builder()
                       .WithCloudSecureConnectionBundle(secureConnectBundlePath)
                       .WithCredentials(username, password)
                       .WithQueryOptions(new QueryOptions().SetConsistencyLevel(ConsistencyLevel.LocalQuorum))
                       .Build()
                       .ConnectAsync(keyspace);

            return session;
        }

        /// <summary>
        /// Saves the new Session object from the parameters provided
        /// </summary>
        /// <param name="username">The Apollo user name</param>
        /// <param name="password">The Apollo password</param>
        /// <param name="keyspace">The keyspace in Apollo</param>
        /// <param name="secureConnectBundlePath">The local file path were the secure connect bundle was saved</param>
        /// <returns>A tuple containing the success of the operation and if it failed the error message</returns>
        public async Task<Tuple<bool, string>> SaveConnection(string username, string password, string keyspace, string secureConnectBundlePath)
        {
            try
            {
                var session = await ConnectToApollo(username, password, keyspace, secureConnectBundlePath);
                Environment.SetEnvironmentVariable("ApolloUsername", username);
                Environment.SetEnvironmentVariable("ApolloPassword", password);
                Environment.SetEnvironmentVariable("ApolloKeyspace", keyspace);
                Environment.SetEnvironmentVariable("SecureConnectBundlePath", secureConnectBundlePath);
                Interlocked.Exchange(ref _session, session);

                //Register the UDT map for the location UDT so that we can use it from LINQ
                _session.UserDefinedTypes.Define(UdtMap.For<location_udt>());
                return new Tuple<bool, string>(true, null);
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, ex.Message);
            }
        }

        /// <summary>
        /// Tests the new Session object from the parameters provided
        /// </summary>
        /// <param name="username">The Apollo user name</param>
        /// <param name="password">The Apollo password</param>
        /// <param name="keyspace">The keyspace in Apollo</param>
        /// <param name="secureConnectBundlePath">The local file path were the secure connect bundle was saved</param>
        /// <returns>A tuple containing the success of the operation and if it failed the error message</returns>
        public async Task<Tuple<bool, string>> TestConnection(string username, string password, string keyspace, string secureConnectBundlePath)
        {
            try
            {
                var session = await ConnectToApollo(username, password, keyspace, secureConnectBundlePath);
                return new Tuple<bool, string>(true, null);
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, ex.Message);
            }
        }
    }
}
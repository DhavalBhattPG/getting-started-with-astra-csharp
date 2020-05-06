# Getting Started with Apollo - C#

This provides an example REST backend built in C# using .NET Core 2.1 for use with the [Getting Started with Apollo UI](https://github.com/DataStax-Examples/getting-started-with-apollo-ui).

Contributors:
* [bechbd](https://github.com/bechbd)
* [msmygit](https://github.com/msmygit)

## Objectives
* How to connect to Apollo via the Secure Connect Bundle
* How to manage a Cassandra Session within a .NET web application

## Project Layout

This sample also contains several interesting files that worth noting specifically:

* [Services/ApolloService.cs](Services/ApolloService.cs) - This file contains all the logic for connecting to the Apollo database using the secure connect bundle.
* [Startup.cs](Startup.cs) - This file contains the logic for adding a singleton for the Session object for reuse across our application.

See [here](https://docs.datastax.com/en/devapp/doc/devapp/driversBestPractices.html#Useasinglesessionobjectperapplication) for additional details on how the session object in Cassandra works and why it is best practice to only have a single Session object per application

* [schema.cql](schema.cql) - The database schema required for the Apollo keyspace
  
* [Controllers/InstrumentController.cs](Controllers/InstrumentsController.cs) - If you would like to see how to implement paging in C# then this would be the place to look.  Paging in Cassandra is different than what you are likely used to so it is beneficial to read [this](https://docs.datastax.com/en/devapp/doc/devapp/driversResultPaging.html) article describing how paging works with Cassandra. 

## How this Sample Works

This is am example .NET Core Web API backend for use with the Apollo Getting Started UI which is found [here](https://github.com/DataStax-Examples/getting-started-with-apollo-ui).

This application serves as the connection between the UI website and an underlying Apollo database.  

It has Swagger installed so once it is running you can look at the Swagger UI here:

```http://localhost:5000/swagger/index.html#/```

### Connecting to Apollo with a Secure Connect Bundle
To see how to connect to Apollo using the Secure Connect Bundle you can look at the `ConnectToApollo()` method in [Services/ApolloService.cs](Services/ApolloService.cs).  In this method you will find the code which:

1. Creates a `Cluster` instance using the builder.
   
    ```var session = Cluster.Builder()```

2. Specifies the local file path to the Secure Connect Bundle ZIP file that has been downloaded from your Apollo Database.  
   
   ```.WithCloudSecureConnectionBundle(secureConnectBundlePath)```
3. Set the username and password for your Apollo Database

    ```.WithCredentials(username, password)```
4. Set the default Consistency Level to `LOCAL_QUORUM`.  `LOCAL_QUORUM` is the only supported consistency level for Apollo queries.
   
   ```.WithQueryOptions(new QueryOptions().SetConsistencyLevel(ConsistencyLevel.LocalQuorum))```
5.  Build the `Cluster` object then connect to your Apollo database specifying the keyspace to use.

    ```.Build().Connect(keyspace);```

Once you have completed all these steps you will now have a fully configured, connected, and ready to run CQL queries. 

### Managing Cassandra Session Within a .NET Web Application
Creation of `Session` objects within an application is an expensive process as they take awhile to initialize and become aware of the clusters topology.  Due to this it is a best practice to create a `Session` object once per application and reuse it throughout the entire lifetime of that application.  When building an ASP.NET Core application as shown this is easily supported through the use of singleton and .NET Core's built in Dependency Injection mechanisms.  

For our Web API endpoints we created a singleton instance of our `ApolloService` object inside the `ConfigureServices()` method in [Startup.cs](Startup.cs) using the following code:

`services.AddSingleton(typeof(Interfaces.IDataStaxService), typeof(Services.ApolloService));`

This code specifies that we are adding a singleton instance of `ApolloService` for any dependency requiring an object instantiating the `Interfaces.IDataStaxService` interface.

To use this within our controllers, we need to specify a property on the constructor of the controller that requires the `Interfaces.IDataStaxService` interface as shown below.

`        
public CredentialsController(IDataStaxService service)
        {
            Service = service;
        }
        `

With each call to the `CredentialsController` the `ApolloService` singleton we created at startup will be passed to the constructor.  This mechanism of dependency injection allows us a simple mechanism to use a single `Session` object throughout the entirety of the application lifecycle.

## Setup and Running

### Prerequisites

* .NET Core 2.1
* An Apollo compatible C# driver, instructions may be found [here](https://helpdocs.datastax.com/aws/dscloud/apollo/dscloudConnectCsharpDriver.html) to install this locally.
* An Apollo database with the CQL schema located in [schema.cql](schema.cql) already added.
* The username, password, keyspace name, and secure connect bundle downloaded from your Apollo Database.  For information on how to obtain these credentials please read the documentation found [here](https://helpdocs.datastax.com/aws/dscloud/apollo/dscloudObtainingCredentials.html).

### Running
This application is a .NET 2.1 web application configured to serve it's web application via the Kestrel web server.  This sample can be run from the root directory using:

```dotnet run```

This will startup the application running on `http://localhost:5000`

You will know that you are up and working when you get the following in your terminal window:

```
Hosting environment: Development
Content root path: /Users/dave.bechberger/Documents/projects/bechbd/getting-started-with-apollo-csharp
Now listening on: http://localhost:5000
Application started. Press Ctrl+C to shut down.
```

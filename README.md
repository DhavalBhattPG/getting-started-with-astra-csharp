# Getting Started with Apollo - C#

This provides an example REST backend built in C# using .NET Core 2.1 for use with the [Getting Started with Apollo UI](https://github.com/DataStax-Examples/getting-started-with-apollo-ui).

Contributors: [bechbd](https://github.com/bechbd)

## Objectives
* How to connect to Apollo via the Secure Connect Bundle
* How to manage a Cassandra Session within a .NET web application

## Project Layout

This sample also contains several interesting files that worth noting specifically:

* [Services/ApolloService.cs](Services/ApolloService.cs) - This file contains all the logic for connecting to the Apollo database using the secure connect bundle.  This is where you would want to look to find out how to connect to an Apollo database.
* [Startup.cs](Startup.cs) - In the `ConfigureServices` method we add a singleton for the Session object as shown here to use as part of the .NET Core Dependency Injection.      
`services.AddSingleton(typeof(Interfaces.IDataStaxService), typeof(Services.ApolloService));`
See [here](https://docs.datastax.com/en/devapp/doc/devapp/driversBestPractices.html#Useasinglesessionobjectperapplication) for additional details on how the session object in Cassandra works and why it is best practice to only have a single Session object per application

* [schema.cql](schema.cql) - The database schema required for the Apollo keyspace
* [Controllers/InstrumentController.cs](Controllers/InstrumentsController.cs) - If you would like to see how to implement paging in C# then this would be the place to look.  Paging in Cassandra is different than what you are likely used to so it is beneficial to read [this](https://docs.datastax.com/en/devapp/doc/devapp/driversResultPaging.html) article describing how paging works with Cassandra. 

## How this Sample Works

This is am example .NET Core Web API backend for use with the Apollo Getting Started UI which is found [here](https://github.com/DataStax-Examples/getting-started-with-apollo-ui).

This application serves as the connection between the UI website and an underlying Apollo database.  

It has Swagger installed so once it is running you can look at the Swagger UI here:

```http://localhost:5000/swagger/index.html#/```

## Setup and Running

### Prerequisites

* .NET Core 2.1
* An Apollo compatible C# driver, instructions may be found [here](https://helpdocs.datastax.com/aws/dscloud/apollo/dscloudConnectCsharpDriver.html) to install this locally.
* An Apollo database with the CQL schema located in [schema.cql](schema.cql) already added.

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

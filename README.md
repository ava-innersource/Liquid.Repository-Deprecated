Liquid Application Framework - Repository
====================================

This repository is part of the [Liquid Application Framework](https://github.com/Avanade/Liquid-Application-Framework), a modern Dotnet Core Application Framework for building cloud native microservices.

The main repository contains the examples and documentation on how to use Liquid.

Liquid.Repository
-----------------
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Avanade_Liquid.Repository&metric=alert_status)](https://sonarcloud.io/dashboard?id=Avanade_Liquid.Repositoty) 

This package contains the repository subsystem of Liquid, along with several databases implementation. In order to use it, add the database package (Liquid.Repository.Mongo for example) you need to your project, along with the specific implementation that you will need.

|Available Cartridges|Badges|
|:--|--|
|[Liquid.Repository.Mongo](https://github.com/Avanade/Liquid.Repository/tree/main/src/Liquid.Repository.Mongo)|[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Avanade_Liquid.Repository.Mongo&metric=alert_status)](https://sonarcloud.io/dashboard?id=Avanade_Liquid.Repository.Mongo)|
|[Liquid.Repository.EntityFramework](https://github.com/Avanade/Liquid.Repository/tree/main/src/Liquid.Repository.EntityFramework)|[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Avanade_Liquid.Repository.EntityFramework&metric=alert_status)](https://sonarcloud.io/dashboard?id=Avanade_Liquid.Repository.EntityFramework)|

# Getting Started
## This is a sample usage with MongoDb cartridge

### Implement your entity and repository using Liquid inheritance
```C#
using Liquid.Repository;
```
```C#
public class MySampleModel : RepositoryEntity<int>
{
    public override int Id { get => base.Id; set => base.Id = value; }
    public string MyProperty { get; set; }
    public int MyProperty2 { get; set; }
    public DateTime MyProperty3 { get; set; }        
}
```
```C#
using Liquid.Repository.Mongo;
```
```C#
//the type of your repository must be the entity related to it.
 interface IMySampleRepository : ILightRepository<MySampleModel, int>
 {
     //Liquid inheritance provides the basic CRUD operations, but you can implement other methods specifically for your application's needs.
 }
```
```C#
//the type of your repository must be the entity related to it.
public class MySampleRepository : MongoRepository<MySampleModel, int>, IMySampleRepository
    {
        public MySampleRepository(ILightTelemetryFactory telemetryFactory, IMongoDataContext dataContext) 
            : base(telemetryFactory, dataContext)
        {
        }
        //Liquid inheritance provides the basic CRUD operations, but you can implement other methods specifically for your application's needs.
    }
```
### Include dependency in domain class constructor, and invoke methods

```C#
public class MySampleDomainClass 
{
    private private IMySampleRepository _repository;

    public MySampleDomainClass(IMySampleRepository repository)
    {
         _repository = repository;
    }
    public async Task Handle()
    {
        //just invoke repository methods
        var entity = await _repository.FindByIdAsync(123);

        await _repository.UpdateAsync(entity);
    }
}
```
Dependency Injection
```C#
services.AddDefaultTelemetry();
services.AddDefaultContext();
services.AddConfigurations(GetType().Assembly);
//the first parameter value must be de configuration section name where connection string is declared.
services.AddMongo("sample", GetType().Assembly);
```
appsettings.json
```Json
"databases": {
      "mongo" : {
        "sample": {
          "connectionString": "",
          "databaseName": "MySampleDb"
        }
      }
    }
  }
```

Liquid Application Framework - Repository
====================================

This repository is part of the [Liquid Application Framework](https://github.com/Avanade/Liquid-Application-Framework), a modern Dotnet Core Application Framework for building cloud native microservices.

The main repository contains the examples and documentation on how to use Liquid.

Liquid.Repository
-----------------
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Avanade_Liquid.Repository&metric=alert_status)](https://sonarcloud.io/dashboard?id=Avanade_Liquid.Repository) 

This package contains the repository subsystem of Liquid, along with several databases implementation. In order to use it, add the database package (Liquid.Repository.Mongo for example) you need to your project, along with the specific implementation that you will need.

|Available Cartridges|Badges|
|:--|--|
|[Liquid.Repository.Mongo](https://github.com/Avanade/Liquid.Repository/tree/main/src/Liquid.Repository.Mongo)|[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Avanade_Liquid.Repository.Mongo&metric=alert_status)](https://sonarcloud.io/dashboard?id=Avanade_Liquid.Repository.Mongo)|
|[Liquid.Repository.EntityFramework](https://github.com/Avanade/Liquid.Repository/tree/main/src/Liquid.Repository.EntityFramework)|[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Avanade_Liquid.Repository.EntityFramework&metric=alert_status)](https://sonarcloud.io/dashboard?id=Avanade_Liquid.Repository.EntityFramework)|

 Getting Started
 ==
>This is a sample usage with MongoDb cartridge

To use Liquid.Repository in your solution, you just need to implement LiquidEntity inheritance, and inject ILiquid.Repository interface, as above
```C#
using Liquid.Repository;
using Liquid.Repository.Mongo.Attributes;
```
```C#
//this annotation is required by mongo cartridge only
[Mongo("SampleCollection", "id", "MySampleDb")]
public class MySampleModel : RepositoryEntity<int>
{
    public override int Id { get => base.Id; set => base.Id = value; }
    public string MyProperty { get; set; }
    public int MyProperty2 { get; set; }
    public DateTime MyProperty3 { get; set; }        
}
```

Include dependency in domain class constructor, and invoke methods

```C#
public class SampleDomainClass 
{
    private ILiquidRepository<SampleEntity, int> _repository;

    public SampleDomainClass((ILiquidRepository<SampleEntity, int> repository)
    {
         _repository = repository;
    }
    public async Task Handle()
    {        
        var entity = await _repository.FindByIdAsync(123);

        await _repository.UpdateAsync(entity);
    }
}
```
Cartridge packages also provides registration methods for your repositories and configurations
```C#
//if you use port packages (Messagin/WebApi) registration methods this first line is unecessary
services.AddLiquidConfiguration();

//this method also register Liquid.Core.TelemetryInterceptor
services.AddLiquidMongoWithTelemetry<SampleEntity, int>();
```
Once the startup and builder is configured using the extension methods as above, it will be necessary to set Liquid Configuration. 
> sample using file provider
```Json
"liquid":{
  "databases": {
    "mongo": {
      "DbSettings": [
        {
          "connectionString": "",
          "databaseName": "MySampleDb"
        }
      ]
    }
  }
}
```
>To get more information of Liquid core features as Configuration and Telemetry see [Liquid.Core Documentation](https://github.com/Avanade/Liquid.Core#readme)

>To get a guide of Web Api implementation using Liquid Application Framework see [Liquid.WebApi Documentation](https://github.com/Avanade/Liquid.WebApi#readme)

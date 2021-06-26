# NET-MongoDB
docs: [link](http://mongodb.github.io/mongo-csharp-driver/2.0/reference/driver/crud/reading/)

```sh
dotnet new sln -n NET-MongoDB
dotnet new webapi -o source/app
dotnet sln add source/app
dotnet add source/app package MongoDB.Driver
dotnet run -p source/app
dotnet watch -p source/app run
```
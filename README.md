# NET-MongoDB

```sh
dotnet new sln -n NET-MongoDB
dotnet new webapi -o source/app
dotnet sln add source/app
dotnet add source/app package MongoDB.Driver
```
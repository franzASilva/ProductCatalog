# Products and Categories

**API RESTful .NET Core 3.1 and EF Core 3.1 (code first) with:**
- Gzip compression;
- Cross-origin resource sharing (CORS);
- Swagger;
- Repository pattern.

**Packages:**
- Microsoft.EntityFrameworkCore;
- Microsoft.EntityFrameworkCore.SqlServer;
- Microsoft.EntityFrameworkCore.Design;
- Swashbuckle.AspNetCore.

**Usage:**
- Install the .NET Core SDK:
> https://dotnet.microsoft.com/download
- Add packages:
```
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Swashbuckle.AspNetCore
```
- Install dotnet ef tool globally:
```
dotnet tool install --global dotnet-ef
```
- Set database connection string (SQL Server): 
> appsettings: connectionString.
- Execute migrations and database update:
```
dotnet ef migrations add initial
dotnet ef database update
```

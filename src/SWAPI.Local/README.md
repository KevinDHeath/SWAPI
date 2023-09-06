## SWAPI.Local
1. Created an ASP.NET Core Web API
Framework: .NET 7.0\
Authentication type: None\
Configure for HTTPS: True\
Enable Docker: False\
Use controllers: True\
Enable OpenAPI support: True\
Do not use top-level statements: True

2. Set IIS Express as the Debug Startup program

3. Created a Publish Profile\
Target: Folder\
Target location: ..\bin\publish\\\
Delete existing files: false\
Configuration: Debug\
Target Framework: net7.0\
Target Runtime: Portable\
Deployment Mode: Framework-dependent

- [Tutorial: Create a web API with ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api)
- [Get started with Swashbuckle and ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle)
- [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore#swashbuckleaspnetcoreswaggergen)
- [Host ASP.NET Core on Windows with IIS](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/iis)

---
- [Host And Publish .NET Core 6 Web API Application On IIS Server](https://www.c-sharpcorner.com/article/host-and-publish-net-core-6-web-api-application-on-iis-server2/)
- [ASP.NET Core Module (ANCM) for IIS](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/aspnet-core-module)
- [Download the ASP.NET Core 7.0 Windows Hosting Bundle Installer](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-7.0.10-windows-hosting-bundle-installer)
- [Debug ASP.NET or ASP.NET Core apps in Visual Studio](https://learn.microsoft.com/en-us/visualstudio/debugger/how-to-enable-debugging-for-aspnet-applications)

---
Notes:

Films = 6\
Person = 82 - missing key 17\
Planets = 60\
Species = 37\
Starships = 36 - missing keys 1, 4, 6, 7, 8, 14, 16, 18, 19, 20, 24, 25, 26, 30, 33, 34, 35, 36, 37, 38, 42, 44, 45, 46, 50, 51, 53, 54, 55, 56, 57, 60, 62, 67, 69, 70, 71, 72, 73\
Vehicles = 39 - missing keys 1, 2, 3, 5, 9, 10, 11, 12, 13, 15, 17, 21, 22, 23, 27, 28, 29, 31, 32, 39, 40, 41, 43, 47, 48, 49, 52, 58, 59, 61, 63, 64, 65, 66, 68, 74, 75

Debugging in Visual Studio 2022

1. Publish
2. Stop the web site in IIS
3. Copy published files (not Web.Config!)
4. Start the web site in IIS
5. Start Visual Studio as Admin
6. Attach to process w3wp.exe (Default App Pool) Attach to: Managed (.NET Core, .NET 5+ code)


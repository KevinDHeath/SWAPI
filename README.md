# SWAPI for C#

This repository contains C# projects that demonstrate how to consume [SWAPI](https://swapi.dev/) _(The Star Wars API)_ resources.

| Project | Description |
| :------ | :---------- |
| [SWAPI.Client](src/SWAPI.Client) | Provides classes to retrieve data from SWAPI. |
| [SWAPI.Data](src/SWAPI.Data) | Provides classes for SWAPI resources. |
| [SWAPI.Examples](src/SWAPI.Examples) | Provides classes to demonstrate the use of the Client. |
| [SWAPI.Local](src/SWAPI.Local) | Provides classes for a local ASP.NET Web API running in IIS. |

The Client and Examples projects are based on [SWapi-CSharp](https://github.com/M-Yankov/SWapi-CSharp/blob/master/LICENSE) with the following changes:
- Upgraded from .Net Framework 4.5 to .NET 8
- Removed the dependency on the Newtonsoft.Json NuGet package and replaced it with System.Text.Json
- Replaced the use of WebRequest (which was deprecated in .NET 6) with HttpClient.
- Removed the sample JSON files in the Example project with a full set of SWAPI data files.


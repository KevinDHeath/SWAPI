## SWAPI

This repository contains C# projects that demonstrate how to use the [SWAPI](https://swapi.dev/) _(The Star Wars API)_.

### Requirements
- Microsoft Visual Studio
- .NET 7

### Contents

| Project | Description |
| :------ | :---------- |
| SWAPI.Client | Provides classes to retrieve data from the API. |
| SWAPI.Data | Provides classes for SWAPI data retrieval. |
| SWAPI.Examples | Provides classes to demonstrate the use of the Client. |
| [SWAPI.Local](.\src\SWAPI.Local) | Local ASP.NET Web API running in IIS. |

### Notes

The Client and Examples are based on [SWapi-CSharp](https://github.com/M-Yankov/SWapi-CSharp) with the following changes:
- Upgraded from .Net Framework 4.5 to .NET 7
- Removed the dependency on the Newtonsoft.Json NuGet package and replaced it with System.Text.Json
- Replaced the use of WebRequest (which was deprecated in .NET 6) with HttpClient.
- Removed the sample JSON files in the Example project with a full set of SWAPI data files.


﻿// Ignore Spelling: Starships

using System.Text.Json.Serialization;

namespace SWAPI.Data.Models;

/// <summary>Provides information on all available resources within the API.</summary>
public class EndPoints
{
	#region Public Constants

	/// <summary>Files endpoint.</summary>
	public const string cFilms = "films/";

	/// <summary>People endpoint.</summary>
	public const string cPeople = "people/";

	/// <summary>Planets endpoint.</summary>
	public const string cPlanets = "planets/";

	/// <summary>Species endpoint.</summary>
	public const string cSpecies = "species/";

	/// <summary>Starships endpoint.</summary>
	public const string cStarships = "starships/";

	/// <summary>Vehicles endpoint.</summary>
	public const string cVehicles = "vehicles/";

	#endregion

	#region Public Properties

	/// <summary>Gets or sets the URL root for Film resources.</summary>
	[JsonPropertyName( "films" )]
	public string? Films { get; set; }

	/// <summary>Gets or sets the URL root for People resources.</summary>
	[JsonPropertyName( "people" )]
	public string? People { get; set; }

	/// <summary>Gets or sets the URL root for Planet resources.</summary>
	[JsonPropertyName( "planets" )]
	public string? Planets { get; set; }

	/// <summary>Gets or sets the URL root for Species resources.</summary>
	[JsonPropertyName( "species" )]
	public string? Species { get; set; }

	/// <summary>Gets or sets the URL root for Starships resources.</summary>
	[JsonPropertyName( "starships" )]
	public string? Starships { get; set; }

	/// <summary>Gets or sets the URL root for Vehicles resources.</summary>
	[JsonPropertyName( "vehicles" )]
	public string? Vehicles { get; set; }

	#endregion
}
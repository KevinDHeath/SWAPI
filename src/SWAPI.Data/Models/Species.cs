// Ignore Spelling: Homeworld

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SWAPI.Data.Models;

/// <summary>Defines a type of person or character in the Star Wars universe.</summary>
public class Species : BaseModel
{
	#region Public Properties

	/// <summary>Gets or sets the name of the species.</summary>
	[Required]
	[JsonPropertyName( "name" )]
	public string? Name { get; set; }

	/// <summary>Gets or sets the classification of the species, such as "mammal" or "reptile".</summary>
	[Required]
	[JsonPropertyName( "classification" )]
	public string? Classification { get; set; }

	/// <summary>Gets or sets the designation of the species, such as "sentient".</summary>
	[Required]
	[JsonPropertyName( "designation" )]
	public string? Designation { get; set; }

	/// <summary>Gets or sets the average height of the species in centimeters.</summary>
	/// <remarks>Can return "unknown" as the value.</remarks>
	[Required]
	[JsonPropertyName( "average_height" )]
	public string? AverageHeight { get; set; }

	/// <summary>Gets or sets the average lifespan of the species in years.</summary>
	/// <remarks>Can return "unknown" as the value.</remarks>
	[Required]
	[JsonPropertyName( "average_lifespan" )]
	public string? AverageLifespan { get; set; }

	/// <summary>Gets or sets a comma-separated string of common eye colors for the species.</summary>
	/// <remarks>Will be "none" if the species does not typically have eyes.</remarks>
	[Required]
	[JsonPropertyName( "eye_colors" )]
	public string? EyeColors { get; set; }

	/// <summary>Gets or sets a comma-separated string of common hair colors for the species.</summary>
	/// <remarks>Will be "none" if the species does not typically have hair.</remarks>
	[Required]
	[JsonPropertyName( "hair_colors" )]
	public string? HairColors { get; set; }

	/// <summary>Gets or sets a comma-separated string of common skin colors for the species.</summary>
	/// <remarks>Will be "none" if the species does not typically have skin.</remarks>
	[Required]
	[JsonPropertyName( "skin_colors" )]
	public string? SkinColors { get; set; }

	/// <summary>Gets or sets the language commonly spoken by the species.</summary>
	[Required]
	[JsonPropertyName( "language" )]
	public string? Language { get; set; }

	/// <summary>Gets or sets the URL of a planet resource, a planet that the species originates from.</summary>
	[JsonPropertyName( "homeworld" )]
	public string? Homeworld { get; set; }

	/// <summary>Gets or sets an array of People URL resources that are a part of the species.</summary>
	[Required]
	[JsonPropertyName( "people" )]
	public ICollection<string>? People { get; set; }

	/// <summary>Gets or sets an array of Film URL resources that the species has appeared in.</summary>
	[Required]
	[JsonPropertyName( "films" )]
	public ICollection<string>? Films { get; set; }

	#endregion

	/// <summary>Initializes a new instance of the Species class.</summary>
	public Species()
	{ }

	/// <summary>Gets the path for extending the base URL.</summary>
	protected override string ResourcePath => Root.cSpecies;
}
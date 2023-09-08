// Ignore Spelling: Starships Homeworld Yavin

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SWAPI.Data.Models;

/// <summary>Defines an individual person or character in the Star Wars universe.</summary>
public class Person : BaseModel
{
	#region Public Properties

	/// <summary>Gets or sets the name of the person.</summary>
	[Required]
	[JsonPropertyName( "name" )]
	public string? Name { get; set; }

	/// <summary>Gets or sets the birth year of the person, using the in-universe standard of BBY or ABY
	/// - Before the Battle of Yavin or After the Battle of Yavin.</summary>
	/// <remarks>The Battle of Yavin is a battle that occurs at the end of Star Wars episode IV: A New Hope.<br/>
	/// Can return "unknown" as the value.</remarks>
	[Required]
	[JsonPropertyName( "birth_year" )]
	public string? BirthYear { get; set; }

	/// <summary>Gets or sets the eye color of the person.</summary>
	/// <remarks>Will be "unknown" if not known or "n/a" if the person does not have an eye.</remarks>
	[Required]
	[JsonPropertyName( "eye_color" )]
	public string? EyeColor { get; set; }

	/// <summary>Gets or sets the gender of the person.</summary>
	/// <remarks>Either "Male", "Female" or "unknown", "n/a" if the person does not have a gender.</remarks>
	[Required]
	[JsonPropertyName( "gender" )]
	public string? Gender { get; set; }

	/// <summary>Gets or sets the hair color of the person.</summary>
	/// <remarks>Will be "unknown" if not known or "n/a" if the person does not have hair.</remarks>
	[Required]
	[JsonPropertyName( "hair_color" )]
	public string? HairColor { get; set; }

	/// <summary>The height of the person in centimeters.</summary>
	/// <remarks>Can return "unknown" as the value.</remarks>
	[Required]
	[JsonPropertyName( "height" )]
	public string? Height { get; set; }

	/// <summary>Gets or sets the mass of the person in kilograms.</summary>
	/// <remarks>Can return "unknown" as the value.</remarks>
	[Required]
	[JsonPropertyName( "mass" )]
	public string? Mass { get; set; }

	/// <summary>Gets or sets the skin color of the person.</summary>
	[Required]
	[JsonPropertyName( "skin_color" )]
	public string? SkinColor { get; set; }

	/// <summary>Gets or sets the URL of a planet resource, a planet that the person was born on or inhabits.</summary>
	[Required]
	[JsonPropertyName( "homeworld" )]
	public string? Homeworld { get; set; }

	/// <summary>Gets or sets an array of film resource URLs that the person has been in.</summary>
	[Required]
	[JsonPropertyName( "films" )]
	public ICollection<string>? Films { get; set; }

	/// <summary>Gets or sets an array of species resource URLs that the person belongs to.</summary>
	[Required]
	[JsonPropertyName( "species" )]
	public ICollection<string>? Species { get; set; }

	/// <summary>Gets or sets an array of starship resource URLs that the person has piloted.</summary>
	[Required]
	[JsonPropertyName( "starships" )]
	public ICollection<string>? Starships { get; set; }

	/// <summary>Gets or sets an array of vehicle resource URLs that the person has piloted.</summary>
	[Required]
	[JsonPropertyName( "vehicles" )]
	public ICollection<string>? Vehicles { get; set; }

	#endregion

	/// <summary>Initializes a new instance of the Person class.</summary>
	[EditorBrowsable( EditorBrowsableState.Never )]
	public Person()
	{ }

	/// <summary>Gets the path for extending the base URL.</summary>
	protected override string ResourcePath => Root.cPeople;
}
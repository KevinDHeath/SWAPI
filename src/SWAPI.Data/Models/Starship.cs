// Ignore Spelling: Atmosphering Hyperdrive starships Starfighter Battlestation Megalight Megalights

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SWAPI.Data.Models;

/// <summary>Defines a transport craft that has hyperdrive capability in the Star Wars universe.</summary>
public class Starship : BaseModel
{
	#region Public Properties

	/// <summary>Gets or sets the name of the starship. The common name, such as "Death Star".</summary>
	[Required]
	[JsonPropertyName( "name" )]
	public string? Name { get; set; }

	/// <summary>Gets or sets the model or official name of the starship.
	/// Such as "T-65 X-wing" or "DS-1 Orbital Battle Station".</summary>
	[Required]
	[JsonPropertyName( "model" )]
	public string? Model { get; set; }

	/// <summary>Gets or sets the class of the starship, such as "Starfighter"
	/// or "Deep Space Mobile Battlestation"</summary>
	[Required]
	[JsonPropertyName( "starship_class" )]
	public string? StarshipClass { get; set; }

	/// <summary>Gets or sets the manufacturer of the starship.</summary>
	/// <remarks>Comma separated if more than one.</remarks>
	[Required]
	[JsonPropertyName( "manufacturer" )]
	public string? Manufacturer { get; set; }

	/// <summary>Gets or sets the cost of the starship new, in galactic credits.</summary>
	/// <remarks>Can return "unknown" as the value.</remarks>
	[Required]
	[JsonPropertyName( "cost_in_credits" )]
	public string? CostInCredits { get; set; }

	/// <summary>Gets or sets the length of the starship in meters.</summary>
	[Required]
	[JsonPropertyName( "length" )]
	public string? Length { get; set; }

	/// <summary>Gets or sets the number of personnel needed to run or pilot the starship.</summary>
	[Required]
	[JsonPropertyName( "crew" )]
	public string? Crew { get; set; }

	/// <summary>Gets or sets the number of non-essential people the starship can transport.</summary>
	/// <remarks>Can return "unknown" as the value.</remarks>
	[Required]
	[JsonPropertyName( "passengers" )]
	public string? Passengers { get; set; }

	/// <summary>Gets or sets the maximum speed of the starship in the atmosphere.</summary>
	/// <remarks>"N/A" if this starship is incapable of atmospheric flight.</remarks>
	[Required]
	[JsonPropertyName( "max_atmosphering_speed" )]
	public string? MaxAtmospheringSpeed { get; set; }

	/// <summary>Gets or sets the class of the starships hyperdrive.</summary>
	[Required]
	[JsonPropertyName( "hyperdrive_rating" )]
	public string? HyperdriveRating { get; set; }

	/// <summary>Gets or sets the Maximum number of Megalights the starship can travel in a standard hour.</summary>
	/// <remarks>A "Megalight" is a standard unit of distance and has never been defined before within the
	/// Star Wars universe. This figure is only really useful for measuring the difference in speed of
	/// starships. We can assume it is similar to AU, the distance between our Sun (Sol) and Earth.<br/>
	/// Can return "unknown" as the value.</remarks>
	[Required]
	[JsonPropertyName( "MGLT" )]
	public string? MegaLights { get; set; }

	/// <summary>Gets or sets the maximum number of kilograms that the starship can transport.</summary>
	/// <remarks>Can return "unknown" as the value.</remarks>
	[Required]
	[JsonPropertyName( "cargo_capacity" )]
	public string? CargoCapacity { get; set; }

	/// <summary>Gets or sets the maximum length of time that the starship can provide consumables
	/// for its entire crew without having to resupply.</summary>
	[Required]
	[JsonPropertyName( "consumables" )]
	public string? Consumables { get; set; }

	/// <summary>Gets or sets an array of Film URL resources that the starship has appeared in.</summary>
	[Required]
	[JsonPropertyName( "films" )]
	public ICollection<string>? Films { get; set; }

	/// <summary>Gets or sets an array of People URL Resources that the starship has been piloted by.</summary>
	[Required]
	[JsonPropertyName( "pilots" )]
	public ICollection<string>? Pilots { get; set; }

	#endregion

	/// <summary>Initializes a new instance of the Starship class.</summary>
	[EditorBrowsable( EditorBrowsableState.Never )]
	public Starship()
	{ }

	/// <summary>Gets the path for extending the base URL.</summary>
	protected override string ResourcePath => Root.cStarships;
}
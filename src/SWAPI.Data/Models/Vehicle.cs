// Ignore Spelling: Atmosphering hyperdrive Repulsorcraft

using System.Text.Json.Serialization;

namespace SWAPI.Data.Models;

/// <summary>Defines a transport craft that does not have hyperdrive capability in the Star Wars universe.</summary>
public class Vehicle : BaseModel
{
	#region Public Properties

	/// <summary>Gets or sets the name of the vehicle. The common name, such as "Sand Crawler"
	/// or "Speeder bike".</summary>
	[JsonPropertyName( "name" )]
	public string? Name { get; set; }

	/// <summary>Gets or sets the model or official name of the vehicle,
	/// such as "All-Terrain Attack Transport".</summary>
	[JsonPropertyName( "model" )]
	public string? Model { get; set; }

	/// <summary>Gets or sets the class of the vehicle, such as "Wheeled" or "Repulsorcraft".</summary>
	[JsonPropertyName( "vehicle_class" )]
	public string? VehicleClass { get; set; }

	/// <summary>Gets or sets the manufacturer of the vehicle.</summary>
	/// <remarks>Comma separated if more than one.</remarks>
	[JsonPropertyName( "manufacturer" )]
	public string? Manufacturer { get; set; }

	/// <summary>Gets or sets the length of the vehicle in meters.</summary>
	[JsonPropertyName( "length" )]
	public string? Length { get; set; }

	/// <summary>Gets or sets the cost of the vehicle new, in Galactic Credits.</summary>
	/// <remarks>Can return "unknown" as the value.</remarks>
	[JsonPropertyName( "cost_in_credits" )]
	public string? CostInCredits { get; set; }

	/// <summary>Gets or sets the number of personnel needed to run or pilot the vehicle.</summary>
	[JsonPropertyName( "crew" )]
	public string? Crew { get; set; }

	/// <summary>Gets or sets the number of non-essential people the vehicle can transport.</summary>
	[JsonPropertyName( "passengers" )]
	public string? Passengers { get; set; }

	/// <summary>Gets or sets the maximum speed of the vehicle in the atmosphere.</summary>
	[JsonPropertyName( "max_atmosphering_speed" )]
	public string? MaxAtmospheringSpeed { get; set; }

	/// <summary>Gets or sets the maximum number of kilograms that the vehicle can transport.</summary>
	/// <remarks>Can return "unknown" or "none" as the value.</remarks>
	[JsonPropertyName( "cargo_capacity" )]
	public string? CargoCapacity { get; set; }

	/// <summary>Gets or sets the maximum length of time that the vehicle can provide consumables
	/// for its entire crew without having to resupply.</summary>
	[JsonPropertyName( "consumables" )]
	public string? Consumables { get; set; }

	/// <summary>Gets or sets an array of Film URL resources that the vehicle has appeared in.</summary>
	[JsonPropertyName( "films" )]
	public ICollection<string>? Films { get; set; }

	/// <summary>Gets or sets an array of People URL resources that the vehicle has been piloted by.</summary>
	[JsonPropertyName( "pilots" )]
	public ICollection<string>? Pilots { get; set; }

	#endregion

	/// <summary>Initializes a new instance of the Vehicle class.</summary>
	public Vehicle()
	{ }

	/// <summary>Gets the path for extending the base URL.</summary>
	protected override string ResourcePath => "vehicles/";
}
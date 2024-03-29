﻿// Ignore Spelling: Atmosphering hyperdrive Repulsorcraft

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SWAPI.Data.Models;

/// <summary>Defines a transport craft that does not have hyperdrive capability in the Star Wars universe.</summary>
public class Vehicle : BaseModel
{
	#region Public Properties

	/// <summary>Gets or sets the name of the vehicle. The common name, such as "Sand Crawler"
	/// or "Speeder bike".</summary>
	[Required]
	[JsonPropertyName( "name" )]
	public string? Name { get; set; }

	/// <summary>Gets or sets the model or official name of the vehicle,
	/// such as "All-Terrain Attack Transport".</summary>
	[Required]
	[JsonPropertyName( "model" )]
	public string? Model { get; set; }

	/// <summary>Gets or sets the class of the vehicle, such as "Wheeled" or "Repulsorcraft".</summary>
	[Required]
	[JsonPropertyName( "vehicle_class" )]
	public string? VehicleClass { get; set; }

	/// <summary>Gets or sets the manufacturer of the vehicle.</summary>
	/// <remarks>Comma separated if more than one.</remarks>
	[Required]
	[JsonPropertyName( "manufacturer" )]
	public string? Manufacturer { get; set; }

	/// <summary>Gets or sets the length of the vehicle in meters.</summary>
	[Required]
	[JsonPropertyName( "length" )]
	public string? Length { get; set; }

	/// <summary>Gets or sets the cost of the vehicle new, in Galactic Credits.</summary>
	/// <remarks>Can return "unknown" as the value.</remarks>
	[Required]
	[JsonPropertyName( "cost_in_credits" )]
	public string? CostInCredits { get; set; }

	/// <summary>Gets or sets the number of personnel needed to run or pilot the vehicle.</summary>
	[Required]
	[JsonPropertyName( "crew" )]
	public string? Crew { get; set; }

	/// <summary>Gets or sets the number of non-essential people the vehicle can transport.</summary>
	[Required]
	[JsonPropertyName( "passengers" )]
	public string? Passengers { get; set; }

	/// <summary>Gets or sets the maximum speed of the vehicle in the atmosphere.</summary>
	[Required]
	[JsonPropertyName( "max_atmosphering_speed" )]
	public string? MaxAtmospheringSpeed { get; set; }

	/// <summary>Gets or sets the maximum number of kilograms that the vehicle can transport.</summary>
	/// <remarks>Can return "unknown" or "none" as the value.</remarks>
	[Required]
	[JsonPropertyName( "cargo_capacity" )]
	public string? CargoCapacity { get; set; }

	/// <summary>Gets or sets the maximum length of time that the vehicle can provide consumables
	/// for its entire crew without having to resupply.</summary>
	[Required]
	[JsonPropertyName( "consumables" )]
	public string? Consumables { get; set; }

	/// <summary>Gets or sets an array of Film URL resources that the vehicle has appeared in.</summary>
	[Required]
	[JsonPropertyName( "films" )]
	public ICollection<string>? Films { get; set; }

	/// <summary>Gets or sets an array of People URL resources that the vehicle has been piloted by.</summary>
	[Required]
	[JsonPropertyName( "pilots" )]
	public ICollection<string>? Pilots { get; set; }

	#endregion

	/// <summary>Initializes a new instance of the Vehicle class.</summary>
	[EditorBrowsable( EditorBrowsableState.Never )]
	public Vehicle()
	{ }

	/// <summary>Gets the path for extending the base URL.</summary>
	protected override string ResourcePath => Root.cVehicles;
}
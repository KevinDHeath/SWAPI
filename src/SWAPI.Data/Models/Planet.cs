using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SWAPI.Data.Models;

/// <summary>Defines a large mass, planet or planetoid in the Star Wars universe.</summary>
public class Planet : BaseModel
{
	#region Public Properties

	/// <summary>Gets or sets the name of the planet.</summary>
	[Required]
	[JsonPropertyName( "name" )]
	public string? Name { get; set; }

	/// <summary>Gets or sets the diameter of the planet in kilometers.</summary>
	/// <remarks>Can return "unknown" as the value.</remarks>
	[Required]
	[JsonPropertyName( "diameter" )]
	public string? Diameter { get; set; }

	/// <summary>Gets or sets the number of standard hours it takes for the planet to complete
	/// a single rotation on its axis.</summary>
	/// <remarks>Can return "unknown" as the value.</remarks>
	[Required]
	[JsonPropertyName( "rotation_period" )]
	public string? RotationPeriod { get; set; }

	/// <summary>Gets or sets the number of standard days it takes for the planet to complete
	/// a single orbit of its local star.</summary>
	/// <remarks>Can return "unknown" as the value.</remarks>
	[Required]
	[JsonPropertyName( "orbital_period" )]
	public string? OrbitalPeriod { get; set; }

	/// <summary>Gets or sets a number denoting the gravity of the planet, where "1" is normal
	/// or 1 standard G. "2" is twice or 2 standard Gs. "0.5" is half or 0.5 standard Gs.</summary>
	/// <remarks>Can return "unknown" as the value.</remarks>
	[Required]
	[JsonPropertyName( "gravity" )]
	public string? Gravity { get; set; }

	/// <summary>Gets or sets the average population of sentient beings inhabiting this planet.</summary>
	/// <remarks>Can return "unknown" as the value.</remarks>
	[Required]
	[JsonPropertyName( "population" )]
	public string? Population { get; set; }

	/// <summary>Gets or sets the climate of the planet.</summary>
	/// <remarks>Comma separated if diverse.</remarks>
	[Required]
	[JsonPropertyName( "climate" )]
	public string? Climate { get; set; }

	/// <summary>Gets or sets the terrain of the planet.</summary>
	/// <remarks>Comma separated if diverse.</remarks>
	[Required]
	[JsonPropertyName( "terrain" )]
	public string? Terrain { get; set; }

	/// <summary>Gets or sets the percentage of the planet surface that is naturally occurring water or bodies of water.</summary>
	[Required]
	[JsonPropertyName( "surface_water" )]
	public string? SurfaceWater { get; set; }

	/// <summary>Gets or sets an array of People URL resources that live on the planet.</summary>
	[Required]
	[JsonPropertyName( "residents" )]
	public ICollection<string>? Residents { get; set; }

	/// <summary>Gets or sets an array of Film URL resources that the planet has appeared in.</summary>
	[Required]
	[JsonPropertyName( "films" )]
	public ICollection<string>? Films { get; set; }

	#endregion

	/// <summary>Initializes a new instance of the Planet class.</summary>
	[EditorBrowsable( EditorBrowsableState.Never )]
	public Planet()
	{ }

	/// <summary>Gets the path for the extending the base URL.</summary>
	protected override string ResourcePath => Root.cPlanets;
}
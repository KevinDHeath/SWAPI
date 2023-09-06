using System.Text.Json.Serialization;

namespace SWAPI.Data.Models;

/// <summary>Contains the results and options to navigate to other pages.</summary>
/// <typeparam name="T">Generic object that inherits BaseModel.</typeparam>
public class Collection<T> where T : BaseModel
{
	#region Public Properties

	/// <summary>Gets or sets the count of the results.</summary>
	[JsonPropertyName( "count" )]
	public int Count { get; set; }

	/// <summary>Gets or sets the next page.</summary>
	[JsonPropertyName( "next" )]
	public string? Next { get; set; }

	/// <summary>Gets or sets the previous page.</summary>
	[JsonPropertyName( "previous" )]
	public string? Previous { get; set; }

	/// <summary>Gets or sets the results downloaded from the data service.</summary>
	[JsonPropertyName( "results" )]
	public ICollection<T>? Results { get; set; }

	#endregion
}
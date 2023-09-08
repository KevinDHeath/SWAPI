using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SWAPI.Data.Models;

/// <summary>Contains the results and options to navigate to other pages.</summary>
/// <typeparam name="T">Generic object that inherits BaseModel.</typeparam>
public class Collection<T> where T : BaseModel
{
	#region Public Properties

	/// <summary>Gets or sets the count of the results.</summary>
	[Required]
	[JsonPropertyName( "count" )]
	public int Count { get; set; }

	/// <summary>Gets or sets the next page.</summary>
	[JsonPropertyName( "next" )]
	public string? Next { get; set; }

	/// <summary>Gets or sets the previous page.</summary>
	[JsonPropertyName( "previous" )]
	public string? Previous { get; set; }

	/// <summary>Gets or sets the results downloaded from the data service.</summary>
	[Required]
	[JsonPropertyName( "results" )]
	public ICollection<T>? Results { get; set; }

	#endregion

	/// <summary>Initializes a new instance of the Collection class.</summary>
	[EditorBrowsable( EditorBrowsableState.Never )]
	public Collection()
	{ }
}
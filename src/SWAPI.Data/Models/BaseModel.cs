using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SWAPI.Data.Models;

/// <summary>Base class for model classes.</summary>
public abstract class BaseModel
{
	#region Public Properties

	/// <summary>Gets or sets the hypermedia URL of the resource.</summary>
	[Required]
	[JsonPropertyName( "url" )]
	public string? Url { get; set; }

	/// <summary>Gets or sets the ISO 8601 date format of the time that the resource was created.</summary>
	[Required]
	[JsonPropertyName( "created" )]
	public DateTime Created { get; set; }

	/// <summary>Gets or sets the ISO 8601 date format of the time that the resource was edited.</summary>
	[Required]
	[JsonPropertyName( "edited" )]
	public DateTime Edited { get; set; }

	#endregion

	/// <summary>Gets the resource path.</summary>
	protected abstract string ResourcePath { get; }

	/// <summary>Gets the relative path of the resource to the base URL.</summary>
	/// <returns>The path as a string.</returns>
	public string GetPath() => ResourcePath;
}
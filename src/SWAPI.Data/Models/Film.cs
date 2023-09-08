// Ignore Spelling: Starships

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SWAPI.Data.Models;

/// <summary>Defines a Star Wars film.</summary>
public class Film : BaseModel
{
	#region Public Properties

	/// <summary>Gets or sets the title of the film.</summary>
	[Required]
	[JsonPropertyName( "title" )]
	public string? Title { get; set; }

	/// <summary>Gets or sets the episode number of the film.</summary>
	[Required]
	[JsonPropertyName( "episode_id" )]
	public int EpisodeId { get; set; }

	/// <summary>Gets or sets the opening paragraphs at the beginning of the film.</summary>
	/// <remarks>The string contains "\r" (carriage return) and "\n" (line feed) characters.</remarks>
	[Required]
	[JsonPropertyName( "opening_crawl" )]
	public string? OpeningCrawl { get; set; }

	/// <summary>Gets or sets the name of the director of the film.</summary>
	[Required]
	[JsonPropertyName( "director" )]
	public string? Director { get; set; }

	/// <summary>Gets or sets the name(s) of the producer(s) of the film.</summary>
	/// <remarks>Comma-separated if multiple.</remarks>
	[Required]
	[JsonPropertyName( "producer" )]
	public string? Producer { get; set; }

	/// <summary>Gets or sets the ISO 8601 date format of the film release in original creator country.</summary>
	[Required]
	[JsonPropertyName( "release_date" )]
	public string? ReleaseDate { get; set; }

	/// <summary>Gets or sets an array of species resource URLs that are in the film.</summary>
	[Required]
	[JsonPropertyName( "species" )]
	public ICollection<string>? Species { get; set; }

	/// <summary>Gets or sets an array of starship resource URLs that are in the film.</summary>
	[Required]
	[JsonPropertyName( "starships" )]
	public ICollection<string>? Starships { get; set; }

	/// <summary>Gets or sets an array of vehicle resource URLs that are in the film.</summary>
	[Required]
	[JsonPropertyName( "vehicles" )]
	public ICollection<string>? Vehicles { get; set; }

	/// <summary>Gets or sets an array of people resource URLs that are in the film.</summary>
	[Required]
	[JsonPropertyName( "characters" )]
	public ICollection<string>? Characters { get; set; }

	/// <summary>Gets or sets an array of planet resource URLs that are in the film.</summary>
	[Required]
	[JsonPropertyName( "planets" )]
	public ICollection<string>? Planets { get; set; }

	#endregion

	/// <summary>Initializes a new instance of the Film class.</summary>
	[EditorBrowsable( EditorBrowsableState.Never )]
	public Film()
	{ }

	/// <inheritdoc/>
	protected override string ResourcePath => Root.cFilms;
}
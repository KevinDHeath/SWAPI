// Ignore Spelling: Starships

using Microsoft.AspNetCore.Mvc;
using SWAPI.Data.Models;
using SWAPI.Local.Stores;

namespace SWAPI.Local.Controllers;

/// <summary>Defines the available actions for star-ships.</summary>
[Route( @"api/starships" )]
[ApiController]
[Produces( "application/json" )]
public class StarshipsController : ControllerBase
{
	private readonly IWebHostEnvironment _env;

	/// <summary>Initializes a new instance of the StarshipsController class.</summary>
	/// <param name="environment">The application's configured IWebHostEnvironment.</param>
	public StarshipsController( IWebHostEnvironment environment )
	{
		_env = environment;
	}

	/// <summary>Gets a collection of star-ships.</summary>
	/// <param name="page">Page number.</param>
	/// <param name="search">Search pattern.</param>
	[HttpGet()]
	[ProducesResponseType( typeof( Collection<Starship> ), StatusCodes.Status200OK )]
	public ActionResult<Collection<Starship>> Get( [FromQuery] int? page, [FromQuery] string? search )
	{
		StarshipStore.Initialize( _env, ControllerContext.HttpContext.Request );
		return Ok( BuildCollection( page, search ) );
	}

	/// <summary>Gets a specific star-ship.</summary>
	/// <param name="id">The star-ship Id.</param>
	/// <returns>The star-ship details.</returns>
	/// <response code="404">Not Found, an empty object will be returned.</response>
	[HttpGet( "{id}" )]
	[ProducesResponseType( StatusCodes.Status200OK )]
	[ProducesResponseType( typeof( Starship ), StatusCodes.Status404NotFound )]
	public ActionResult<Starship> GetById( int id )
	{
		StarshipStore.Initialize( _env, ControllerContext.HttpContext.Request );
		return StarshipStore.Starships.ContainsKey( id ) ?
			(ActionResult<Starship>)Ok( StarshipStore.Starships[id] ) :
			(ActionResult<Starship>)NotFound( new Starship() );
	}

	#region Private Methods

	private Collection<Starship> BuildCollection( int? page, string? search )
	{
		page ??= 1;
		search ??= string.Empty;

		ICollection<Starship> results = search.Length > 0
			? StarshipStore.Starships.Values.Where( p =>
				( p.Name is not null && p.Name.Contains( search, StringComparison.OrdinalIgnoreCase ) ) ||
				( p.Model is not null && p.Model.Contains( search, StringComparison.OrdinalIgnoreCase ) ) )
				.ToList()
			: StarshipStore.Starships.Values;

		string url = StarshipStore.GetUrl( ControllerContext.HttpContext.Request );
		Collection<Starship> rtn = BaseStorage.DoPaging( page.Value, search, results, url );

		return rtn;
	}

	#endregion
}
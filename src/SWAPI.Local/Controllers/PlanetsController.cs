using Microsoft.AspNetCore.Mvc;
using SWAPI.Data.Models;
using SWAPI.Local.Stores;

namespace SWAPI.Local.Controllers;

/// <summary>Defines the available actions for planets.</summary>
[Route( @"api/planets" )]
[ApiController]
[Produces( "application/json" )]
public class PlanetsController : ControllerBase
{
	private readonly IWebHostEnvironment _env;

	/// <summary>Initializes a new instance of the PlanetsController class.</summary>
	/// <param name="environment">The application's configured IWebHostEnvironment.</param>
	public PlanetsController( IWebHostEnvironment environment )
	{
		_env = environment;
	}

	/// <summary>Gets a collection of planets.</summary>
	/// <param name="page">Page number.</param>
	/// <param name="search">Search pattern.</param>
	/// <returns>A collection of planet details.</returns>
	[HttpGet()]
	[ProducesResponseType( typeof( Collection<Planet> ), StatusCodes.Status200OK )]
	public ActionResult<Collection<Planet>> Get( [FromQuery] int? page, [FromQuery] string? search )
	{
		PlanetStore.Initialize( _env, ControllerContext.HttpContext.Request );
		return Ok( BuildCollection( page, search ) );
	}

	/// <summary>Gets a specific planet.</summary>
	/// <param name="id">The planet Id.</param>
	/// <returns>The planet details.</returns>
	/// <response code="404">Not Found, an empty object will be returned.</response>
	[HttpGet( "{id}" )]
	[ProducesResponseType( StatusCodes.Status200OK )]
	[ProducesResponseType( typeof( Planet ), StatusCodes.Status404NotFound )]
	public ActionResult<Planet> GetById( int id )
	{
		PlanetStore.Initialize( _env, ControllerContext.HttpContext.Request );
		return PlanetStore.Planets.ContainsKey( id ) ?
			(ActionResult<Planet>)Ok( PlanetStore.Planets[id] ) :
			(ActionResult<Planet>)NotFound( new Planet() );
	}

	#region Private Methods

	private Collection<Planet> BuildCollection( int? page, string? search )
	{
		page ??= 1;
		search ??= string.Empty;

		ICollection<Planet> results = search.Length > 0
			? PlanetStore.Planets.Values.Where( p => p.Name is not null &&
				p.Name.Contains( search, StringComparison.OrdinalIgnoreCase ) ).ToList()
			: PlanetStore.Planets.Values;

		string url = PlanetStore.GetUrl( ControllerContext.HttpContext.Request );
		Collection<Planet> rtn = BaseStorage.DoPaging( page.Value, search, results, url );

		return rtn;
	}

	#endregion
}

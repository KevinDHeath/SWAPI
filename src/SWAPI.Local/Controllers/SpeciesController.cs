using Microsoft.AspNetCore.Mvc;
using SWAPI.Data.Models;
using SWAPI.Local.Stores;

namespace SWAPI.Local.Controllers;

/// <summary>Defines the available actions for species.</summary>
[Route( @"api/species" )]
[ApiController]
[Produces( "application/json" )]
public class SpeciesController : ControllerBase
{
	private readonly IWebHostEnvironment _env;

	/// <summary>Initializes a new instance of the SpeciesController class.</summary>
	/// <param name="environment">The application's configured IWebHostEnvironment.</param>
	public SpeciesController( IWebHostEnvironment environment )
	{
		_env = environment;
	}

	/// <summary>Gets a collection of species.</summary>
	/// <param name="page">Page number.</param>
	/// <param name="search">Search pattern.</param>
	[HttpGet()]
	[ProducesResponseType( typeof( Collection<Species> ), StatusCodes.Status200OK )]
	public ActionResult<Collection<Species>> Get( [FromQuery] int? page, [FromQuery] string? search )
	{
		SpeciesStore.Initialize( _env, ControllerContext.HttpContext.Request );
		return Ok( BuildCollection( page, search ) );
	}

	/// <summary>Gets a specific species.</summary>
	/// <param name="id">The species Id.</param>
	/// <returns>The species details.</returns>
	/// <response code="404">Not Found, an empty object will be returned.</response>
	[HttpGet( "{id}" )]
	[ProducesResponseType( StatusCodes.Status200OK )]
	[ProducesResponseType( typeof( Species ), StatusCodes.Status404NotFound )]
	public ActionResult<Species> GetById( int id )
	{
		SpeciesStore.Initialize( _env, ControllerContext.HttpContext.Request );
		return SpeciesStore.Species.ContainsKey( id ) ?
			(ActionResult<Species>)Ok( SpeciesStore.Species[id] ) :
			(ActionResult<Species>)NotFound( new Species() );
	}

	#region Private Methods

	private Collection<Species> BuildCollection( int? page, string? search )
	{
		page ??= 1;
		search ??= string.Empty;

		ICollection<Species> results = search.Length > 0
			? SpeciesStore.Species.Values.Where( p => p.Name is not null &&
				p.Name.Contains( search, StringComparison.OrdinalIgnoreCase ) ).ToList()
			: SpeciesStore.Species.Values;

		string url = SpeciesStore.GetUrl( ControllerContext.HttpContext.Request );
		Collection<Species> rtn = BaseStorage.DoPaging( page.Value, search, results, url );

		return rtn;
	}

	#endregion
}
using Microsoft.AspNetCore.Mvc;
using SWAPI.Data.Models;
using SWAPI.Local.Stores;

namespace SWAPI.Local.Controllers;

/// <summary>Defines the available actions for films.</summary>
[Route( @"api/films" )]
[ApiController]
[Produces( "application/json" )]
public class FilmsController : ControllerBase
{
	private readonly IWebHostEnvironment _env;

	/// <summary>Initializes a new instance of the FilmsController class.</summary>
	/// <param name="environment">The application's configured IWebHostEnvironment.</param>
	public FilmsController( IWebHostEnvironment environment )
	{
		_env = environment;
	}

	/// <summary>Gets a collection of films.</summary>
	/// <param name="page">Page number.</param>
	/// <param name="search">Search pattern.</param>
	/// <returns>A collection of film details.</returns>
	[HttpGet()]
	[ProducesResponseType( typeof( Collection<Film> ), StatusCodes.Status200OK )]
	public ActionResult<Collection<Film>> Get( [FromQuery] int? page, [FromQuery] string? search )
	{
		FilmStore.Initialize( _env, ControllerContext.HttpContext.Request );
		return Ok( BuildCollection( page, search ) );
	}

	/// <summary>Gets a specific film.</summary>
	/// <param name="id">The film Id.</param>
	/// <returns>The film details.</returns>
	/// <response code="404">Not Found, an empty object will be returned.</response>
	[HttpGet( "{id}" )]
	[ProducesResponseType( StatusCodes.Status200OK )]
	[ProducesResponseType( typeof( Film ), StatusCodes.Status404NotFound )]
	public ActionResult<Film> GetById( int id )
	{
		FilmStore.Initialize( _env, ControllerContext.HttpContext.Request );
		return FilmStore.Films.ContainsKey( id ) ?
			(ActionResult<Film>)Ok( FilmStore.Films[id] ) :
			(ActionResult<Film>)NotFound( new Film() );
	}

	#region Private Methods

	private Collection<Film> BuildCollection( int? page, string? search )
	{
		page ??= 1;
		search ??= string.Empty;

		ICollection<Film> results = search.Length > 0
			? FilmStore.Films.Values.Where( p => p.Title is not null &&
				p.Title.Contains( search, StringComparison.OrdinalIgnoreCase ) ).ToList()
			: FilmStore.Films.Values;

		string url = FilmStore.GetUrl( ControllerContext.HttpContext.Request );
		Collection<Film> rtn = BaseStorage.DoPaging( page.Value, search, results, url );

		return rtn;
	}

	#endregion
}
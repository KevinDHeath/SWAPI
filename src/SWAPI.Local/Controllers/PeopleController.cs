using Microsoft.AspNetCore.Mvc;
using SWAPI.Data.Models;
using SWAPI.Local.Stores;

namespace SWAPI.Local.Controllers;

/// <summary>Defines the available actions for people.</summary>
[Route( @"api/people" )]
[ApiController]
[Produces( "application/json" )]
public class PeopleController : ControllerBase
{
	private readonly IWebHostEnvironment _env;

	/// <summary>Initializes a new instance of the PeopleController class.</summary>
	/// <param name="environment">The application's configured IWebHostEnvironment.</param>
	public PeopleController( IWebHostEnvironment environment )
	{
		_env = environment;
	}

	/// <summary>Gets a collection of people.</summary>
	/// <param name="page">Page number.</param>
	/// <param name="search">Search pattern.</param>
	[HttpGet()]
	[ProducesResponseType( typeof( Collection<Person> ), StatusCodes.Status200OK )]
	public ActionResult<Collection<Person>> Get( [FromQuery] int? page, [FromQuery] string? search )
	{
		PersonStore.Initialize( _env, ControllerContext.HttpContext.Request );
		return Ok( BuildCollection( page, search ) );
	}

	/// <summary>Gets a specific person.</summary>
	/// <param name="id">The person Id.</param>
	/// <returns>The person details.</returns>
	/// <response code="404">Not Found, an empty object will be returned.</response>
	[HttpGet( "{id}" )]
	[ProducesResponseType( StatusCodes.Status200OK )]
	[ProducesResponseType( typeof( Person), StatusCodes.Status404NotFound )]
	public ActionResult<Person> GetById( int id )
	{
		PersonStore.Initialize( _env, ControllerContext.HttpContext.Request );
		return PersonStore.People.ContainsKey( id ) ?
			(ActionResult<Person>)Ok( PersonStore.People[id] ) :
			(ActionResult<Person>)NotFound( new Person() );
	}

	#region Private Methods

	private Collection<Person> BuildCollection( int? page, string? search )
	{
		page ??= 1;
		search ??= string.Empty;

		ICollection<Person> results = search.Length > 0
			? PersonStore.People.Values.Where( p => p.Name is not null &&
				p.Name.Contains( search, StringComparison.OrdinalIgnoreCase ) ).ToList()
			: PersonStore.People.Values;

		string url = PersonStore.GetUrl( ControllerContext.HttpContext.Request );
		Collection<Person> rtn = BaseStorage.DoPaging( page.Value, search, results, url );

		return rtn;
	}

	#endregion
}
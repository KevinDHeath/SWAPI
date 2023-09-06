using Microsoft.AspNetCore.Mvc;
using SWAPI.Data.Models;
using SWAPI.Local.Stores;

namespace SWAPI.Local.Controllers;

/// <summary>Defines the available actions for vehicles.</summary>
[Route( @"api/vehicles" )]
[ApiController]
public class VehiclesController : ControllerBase
{
	private readonly IWebHostEnvironment _env;

	/// <summary>Initializes a new instance of the VehiclesController class.</summary>
	/// <param name="environment">The application's configured IWebHostEnvironment.</param>
	public VehiclesController( IWebHostEnvironment environment )
	{
		_env = environment;
	}

	/// <summary>Gets a collection of vehicles.</summary>
	/// <param name="page">Page number.</param>
	/// <param name="search">Search pattern.</param>
	[HttpGet()]
	[ProducesResponseType( typeof( Collection<Vehicle> ), StatusCodes.Status200OK )]
	public ActionResult<Collection<Vehicle>> Get( [FromQuery] int? page, [FromQuery] string? search )
	{
		VehicleStore.Initialize( _env, ControllerContext.HttpContext.Request );
		return Ok( BuildCollection( page, search ) );
	}

	/// <summary>Gets a specific vehicle.</summary>
	/// <param name="id">The vehicle Id.</param>
	/// <returns>The vehicle details.</returns>
	/// <response code="404">Not Found, an empty object will be returned.</response>
	[HttpGet( "{id}" )]
	[ProducesResponseType( StatusCodes.Status200OK )]
	[ProducesResponseType( typeof( Vehicle ), StatusCodes.Status404NotFound )]
	public ActionResult<Vehicle> GetById( int id )
	{
		VehicleStore.Initialize( _env, ControllerContext.HttpContext.Request );
		return VehicleStore.Vehicles.ContainsKey( id ) ?
			(ActionResult<Vehicle>)Ok( VehicleStore.Vehicles[id] ) :
			(ActionResult<Vehicle>)NotFound( new Vehicle() );
	}

	#region Private Methods

	private Collection<Vehicle> BuildCollection( int? page, string? search )
	{
		page ??= 1;
		search ??= string.Empty;

		ICollection<Vehicle> results = search.Length > 0
			? VehicleStore.Vehicles.Values.Where( p =>
				( p.Name is not null && p.Name.Contains( search, StringComparison.OrdinalIgnoreCase ) ) ||
				( p.Model is not null && p.Model.Contains( search, StringComparison.OrdinalIgnoreCase ) ) )
				.ToList()
			: VehicleStore.Vehicles.Values;

		string url = VehicleStore.GetUrl( ControllerContext.HttpContext.Request );
		Collection<Vehicle> rtn = BaseStorage.DoPaging( page.Value, search, results, url );

		return rtn;
	}

	#endregion
}
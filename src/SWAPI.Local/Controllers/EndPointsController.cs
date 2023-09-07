using Microsoft.AspNetCore.Mvc;
using SWAPI.Data.Models;
using SWAPI.Local.Stores;

namespace SWAPI.Local.Controllers;

/// <summary>Defines the available resource endpoints.</summary>
[Route( @"api" )]
[ApiController]
[Produces( "application/json" )]
public class EndPointsController : ControllerBase
{
	/// <summary>Initializes a new instance of the EndPointsController class.</summary>
	public EndPointsController()
	{ }

	/// <summary>Provides information on all available resources within the API.</summary>
	/// <returns>The film details.</returns>
	[HttpGet()]
	[ProducesResponseType( StatusCodes.Status200OK )]
	public ActionResult<EndPoints> Get()
	{
		EndPointsStore.Initialize( ControllerContext.HttpContext.Request );
		return (ActionResult<EndPoints>)Ok( EndPointsStore.EndPoints );
	}
}
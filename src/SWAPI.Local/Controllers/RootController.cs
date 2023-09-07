using Microsoft.AspNetCore.Mvc;
using SWAPI.Data.Models;
using SWAPI.Local.Stores;

namespace SWAPI.Local.Controllers;

/// <summary>Defines the available resource endpoints.</summary>
[Route( @"api" )]
[ApiController]
[Produces( "application/json" )]
public class RootController : ControllerBase
{
	/// <summary>Initializes a new instance of the EndPointsController class.</summary>
	public RootController()
	{ }

	/// <summary>Provides information on all available resources within the API.</summary>
	/// <returns>The film details.</returns>
	[HttpGet()]
	[ProducesResponseType( StatusCodes.Status200OK )]
	public ActionResult<Root> Get()
	{
		RootStore.Initialize( ControllerContext.HttpContext.Request );
		return (ActionResult<Root>)Ok( RootStore.EndPoints );
	}
}
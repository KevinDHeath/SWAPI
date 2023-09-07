using SWAPI.Data.Models;

namespace SWAPI.Local.Stores;

internal class EndPointsStore : BaseStorage
{
	#region Internal Properties

	private static EndPoints? _endPoints;
	internal static EndPoints EndPoints
	{
		get => _endPoints is null ? new EndPoints() : _endPoints;
		private set => _endPoints = value;
	}

	#endregion

	#region Internal Methods

	internal static void Initialize( IWebHostEnvironment environment, HttpRequest request )
	{
		if( _endPoints is not null ) { return; }

		string rootUrl = GetResourceUrl( request ) + "/";
		_endPoints = new EndPoints
		{
			Films = rootUrl + EndPoints.cFilms,
			People = rootUrl + EndPoints.cPeople,
			Planets = rootUrl + EndPoints.cPlanets,
			Species = rootUrl + EndPoints.cSpecies,
			Starships = rootUrl + EndPoints.cStarships,
			Vehicles = rootUrl + EndPoints.cVehicles
		};
		return;
	}

	#endregion
}
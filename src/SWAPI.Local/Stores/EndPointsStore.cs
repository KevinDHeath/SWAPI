using SWAPI.Data.Models;

namespace SWAPI.Local.Stores;

internal class EndPointsStore : BaseStorage
{
	private static EndPoints? _endPoints;
	private static readonly object _endPointsLock = new();
	private static bool _endPointsLoad = false;

	#region Internal Properties

	internal static EndPoints EndPoints
	{
		get => _endPoints is null ? new EndPoints() : _endPoints;
		private set => _endPoints = value;
	}

	#endregion

	#region Internal Methods

	internal static void Initialize( HttpRequest request )
	{
		if( _endPoints is not null ) { return; }
		try
		{
			string rootUrl = GetResourceUrl( request ) + cSep;

			// Load endpoints data
			Monitor.Enter( _endPointsLock, ref _endPointsLoad );
			_endPoints = new EndPoints
			{
				Films = rootUrl + EndPoints.cFilms,
				People = rootUrl + EndPoints.cPeople,
				Planets = rootUrl + EndPoints.cPlanets,
				Species = rootUrl + EndPoints.cSpecies,
				Starships = rootUrl + EndPoints.cStarships,
				Vehicles = rootUrl + EndPoints.cVehicles
			};
		}
		catch( Exception ) { }
		finally
		{
			if( _endPointsLoad )
			{
				Monitor.Exit( _endPointsLock );
				_endPointsLoad = false;
			}
		}
		return;
	}

	#endregion
}
using SWAPI.Data.Models;

namespace SWAPI.Local.Stores;

internal class RootStore : BaseStorage
{
	private static Root? _endPoints;
	private static readonly object _endPointsLock = new();
	private static bool _endPointsLoad = false;

	#region Internal Properties

	internal static Root EndPoints
	{
		get => _endPoints is null ? new Root() : _endPoints;
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
			_endPoints = new Root
			{
				Films = rootUrl + Root.cFilms,
				People = rootUrl + Root.cPeople,
				Planets = rootUrl + Root.cPlanets,
				Species = rootUrl + Root.cSpecies,
				Starships = rootUrl + Root.cStarships,
				Vehicles = rootUrl + Root.cVehicles
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
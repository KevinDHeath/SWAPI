using SWAPI.Data;
using SWAPI.Data.Models;

namespace SWAPI.Local.Stores;

internal class PlanetStore : BaseStorage
{
	private static IDictionary<int, Planet>? _planets;
	private static string? _resourceUrl;
	private static readonly object _planetsLock = new();
	private static bool _planetsLoad = false;

	#region Internal Properties

	internal static IDictionary<int, Planet> Planets
	{
		get => _planets is null ? new Dictionary<int, Planet>() : _planets;
		private set => _planets = value;
	}

	#endregion

	#region Internal Methods

	internal static string GetUrl( HttpRequest request )
	{
		_resourceUrl ??= GetResourceUrl( request );
		return _resourceUrl;
	}

	internal static void Initialize( IWebHostEnvironment environment, HttpRequest request )
	{
		if( _planets is not null ) { return; }
		try
		{
			_resourceUrl ??= GetResourceUrl( request );
			string rootUrl = GetRootUrl( _resourceUrl );

			// Load planets data
			Monitor.Enter( _planetsLock, ref _planetsLoad );
			ICollection<Planet> list = Factory.LoadPlanets( GetIISPath( environment ) );
			Planets = new Dictionary<int, Planet>();
			foreach( var item in list )
			{
				int? key = GetItemKey( item.Url );
				if( key.HasValue && !Planets.ContainsKey( key.Value ) )
				{
					item.Url = SetUrlPrefix( rootUrl, item.Url );
					ProcessArray( item.Residents as List<string>, rootUrl );
					ProcessArray( item.Films as List<string>, rootUrl );
					Planets.Add( key.Value, item );
				}
			}
		}
		catch( Exception ) { }
		finally
		{
			if( _planetsLoad )
			{
				Monitor.Exit( _planetsLock );
				_planetsLoad = false;
			}
		}
		return;
	}

	#endregion
}
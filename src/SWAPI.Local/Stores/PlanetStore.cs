using SWAPI.Data;
using SWAPI.Data.Models;

namespace SWAPI.Local.Stores;

internal class PlanetStore : BaseStorage
{
	#region Internal Properties

	private static IDictionary<int, Planet>? _planets;
	internal static IDictionary<int, Planet> Planets
	{
		get => _planets is null ? new Dictionary<int, Planet>() : _planets;
		private set => _planets = value;
	}

	#endregion

	#region Internal Methods

	private static string? _resourceUrl;
	internal static string GetUrl( HttpRequest request )
	{
		_resourceUrl ??= GetResourceUrl( request );
		return _resourceUrl;
	}

	internal static void Initialize( IWebHostEnvironment environment, HttpRequest request )
	{
		_resourceUrl ??= GetResourceUrl( request );
		if( _planets is not null ) { return; }

		ICollection<Planet> list = Factory.LoadPlanets( GetIISPath( environment ) );
		string rootUrl = GetRootUrl( _resourceUrl );
		Planets = new Dictionary<int, Planet>();

		foreach( var item in list )
		{
			int? key = GetItemKey( item.Url );
			if( key.HasValue && !Planets.ContainsKey( key.Value ) )
			{
				item.Url = rootUrl + item.Url;
				ProcessArray( item.Residents as List<string>, rootUrl );
				ProcessArray( item.Films as List<string>, rootUrl );
				Planets.Add( key.Value, item );
			}
		}
		return;
	}

	#endregion
}
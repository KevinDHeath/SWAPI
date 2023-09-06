using SWAPI.Data;
using SWAPI.Data.Models;

namespace SWAPI.Local.Stores;

internal class FilmStore : BaseStorage
{
	#region Internal Properties

	private static IDictionary<int, Film>? _films;
	internal static IDictionary<int, Film> Films
	{
		get => _films is null ? new Dictionary<int, Film>() : _films;
		private set => _films = value;
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
		if( _films is not null ) { return; }

		ICollection<Film> list = Factory.LoadFilms( GetIISPath( environment ) );
		string rootUrl = GetRootUrl( _resourceUrl );
		Films = new Dictionary<int, Film>();

		foreach( var item in list )
		{
			int? key = GetItemKey( item.Url );
			if( key.HasValue && !Films.ContainsKey( key.Value ) )
			{
				item.Url = rootUrl + item.Url;
				ProcessArray( item.Species as List<string>, rootUrl );
				ProcessArray( item.Starships as List<string>, rootUrl );
				ProcessArray( item.Vehicles as List<string>, rootUrl );
				ProcessArray( item.Characters as List<string>, rootUrl );
				ProcessArray( item.Planets as List<string>, rootUrl );
				Films.Add( key.Value, item );
			}
		}
		return;
	}

	#endregion
}
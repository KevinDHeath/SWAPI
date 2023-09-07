using SWAPI.Data;
using SWAPI.Data.Models;

namespace SWAPI.Local.Stores;

internal class FilmStore : BaseStorage
{
	private static IDictionary<int, Film>? _films;
	private static string? _resourceUrl;
	private static readonly object _filmsLock = new();
	private static bool _filmsLoad = false;

	#region Internal Properties

	internal static IDictionary<int, Film> Films
	{
		get => _films is null ? new Dictionary<int, Film>() : _films;
		private set => _films = value;
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
		if( _films is not null ) { return; }
		try
		{
			_resourceUrl ??= GetResourceUrl( request );
			string rootUrl = GetRootUrl( _resourceUrl );

			// Load films data
			Monitor.Enter( _filmsLock, ref _filmsLoad );
			ICollection<Film> list = Factory.LoadFilms( GetIISPath( environment ) );
			Films = new Dictionary<int, Film>();
			foreach( var item in list )
			{
				int? key = GetItemKey( item.Url );
				if( key.HasValue && !Films.ContainsKey( key.Value ) )
				{
					item.Url = SetUrlPrefix( rootUrl, item.Url );
					ProcessArray( item.Species as List<string>, rootUrl );
					ProcessArray( item.Starships as List<string>, rootUrl );
					ProcessArray( item.Vehicles as List<string>, rootUrl );
					ProcessArray( item.Characters as List<string>, rootUrl );
					ProcessArray( item.Planets as List<string>, rootUrl );
					Films.Add( key.Value, item );
				}
			}
		}
		catch( Exception ) { }
		finally
		{
			if( _filmsLoad )
			{
				Monitor.Exit( _filmsLock );
				_filmsLoad = false;
			}
		}
		return;
	}

	#endregion
}
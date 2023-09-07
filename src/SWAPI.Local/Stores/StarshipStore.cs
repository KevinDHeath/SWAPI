// Ignore Spelling: Starships

using SWAPI.Data;
using SWAPI.Data.Models;

namespace SWAPI.Local.Stores;

internal class StarshipStore : BaseStorage
{
	private static IDictionary<int, Starship>? _starships;
	private static string? _resourceUrl;
	private static readonly object _starshipsLock = new();
	private static bool _starshipsLoad = false;

	#region Internal Properties

	internal static IDictionary<int, Starship> Starships
	{
		get => _starships is null ? new Dictionary<int, Starship>() : _starships;
		private set => _starships = value;
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
		if( _starships is not null ) { return; }
		try
		{
			_resourceUrl ??= GetResourceUrl( request );
			string rootUrl = GetRootUrl( _resourceUrl );

			// Load starships data
			Monitor.Enter( _starshipsLock, ref _starshipsLoad );
			ICollection<Starship> list = Factory.LoadStarships( GetIISPath( environment ) );
			Starships = new Dictionary<int, Starship>();
			foreach( var item in list )
			{
				int? key = GetItemKey( item.Url );
				if( key.HasValue && !Starships.ContainsKey( key.Value ) )
				{
					item.Url = SetUrlPrefix( rootUrl, item.Url );
					ProcessArray( item.Films as List<string>, rootUrl );
					ProcessArray( item.Pilots as List<string>, rootUrl );
					Starships.Add( key.Value, item );
				}
			}
		}
		catch( Exception ) { }
		finally
		{
			if( _starshipsLoad )
			{
				Monitor.Exit( _starshipsLock );
				_starshipsLoad = false;
			}
		}
		return;
	}

	#endregion
}
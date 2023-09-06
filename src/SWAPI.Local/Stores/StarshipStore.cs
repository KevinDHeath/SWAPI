using SWAPI.Data;
using SWAPI.Data.Models;

namespace SWAPI.Local.Stores;

internal class StarshipStore : BaseStorage
{
	#region Internal Properties

	private static IDictionary<int, Starship>? _starships;
	internal static IDictionary<int, Starship> Starships
	{
		get => _starships is null ? new Dictionary<int, Starship>() : _starships;
		private set => _starships = value;
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
		if( _starships is not null ) { return; }

		ICollection<Starship> list = Factory.LoadStarships( GetIISPath( environment ) );
		string rootUrl = GetRootUrl( _resourceUrl );
		Starships = new Dictionary<int, Starship>();

		foreach( var item in list )
		{
			int? key = GetItemKey( item.Url );
			if( key.HasValue && !Starships.ContainsKey( key.Value ) )
			{
				item.Url = rootUrl + item.Url;
				ProcessArray( item.Films as List<string>, rootUrl );
				ProcessArray( item.Pilots as List<string>, rootUrl );
				Starships.Add( key.Value, item );
			}
		}
		return;
	}

	#endregion
}
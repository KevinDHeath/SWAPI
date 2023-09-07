using SWAPI.Data;
using SWAPI.Data.Models;

namespace SWAPI.Local.Stores;

internal class SpeciesStore : BaseStorage
{
	private static IDictionary<int, Species>? _species;
	private static string? _resourceUrl;
	private static readonly object _speciesLock = new();
	private static bool _speciesLoad = false;

	#region Internal Properties

	internal static IDictionary<int, Species> Species
	{
		get => _species is null ? new Dictionary<int, Species>() : _species;
		private set => _species = value;
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
		if( _species is not null ) { return; }
		try
		{
			_resourceUrl ??= GetResourceUrl( request );
			string rootUrl = GetRootUrl( _resourceUrl );

			// Load species data
			Monitor.Enter( _speciesLock, ref _speciesLoad );
			ICollection<Species> list = Factory.LoadSpecies( GetIISPath( environment ) );
			Species = new Dictionary<int, Species>();
			foreach( var item in list )
			{
				int? key = GetItemKey( item.Url );
				if( key.HasValue && !Species.ContainsKey( key.Value ) )
				{
					item.Url = SetUrlPrefix( rootUrl, item.Url );
					item.Homeworld = SetUrlPrefix( rootUrl, item.Homeworld );
					ProcessArray( item.People as List<string>, rootUrl );
					ProcessArray( item.Films as List<string>, rootUrl );
					Species.Add( key.Value, item );
				}
			}
		}
		catch( Exception ) { }
		finally
		{
			if( _speciesLoad )
			{
				Monitor.Exit( _speciesLock );
				_speciesLoad = false;
			}
		}
		return;
	}

	#endregion
}
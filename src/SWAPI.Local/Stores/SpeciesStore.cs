using SWAPI.Data;
using SWAPI.Data.Models;

namespace SWAPI.Local.Stores;

internal class SpeciesStore : BaseStorage
{
	#region Internal Properties

	private static IDictionary<int, Species>? _species;
	internal static IDictionary<int, Species> Species
	{
		get => _species is null ? new Dictionary<int, Species>() : _species;
		private set => _species = value;
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
		if( _species is not null ) { return; }

		ICollection<Species> list = Factory.LoadSpecies( GetIISPath( environment ) );
		string rootUrl = GetRootUrl( _resourceUrl );
		Species = new Dictionary<int, Species>();

		foreach( var item in list )
		{
			int? key = GetItemKey( item.Url );
			if( key.HasValue && !Species.ContainsKey( key.Value ) )
			{
				item.Url = rootUrl + item.Url;
				item.Homeworld = !string.IsNullOrWhiteSpace( item.Homeworld ) ?
					rootUrl + item.Homeworld : string.Empty;
				ProcessArray( item.People as List<string>, rootUrl );
				ProcessArray( item.Films as List<string>, rootUrl );
				Species.Add( key.Value, item );
			}
		}
		return;
	}

	#endregion
}
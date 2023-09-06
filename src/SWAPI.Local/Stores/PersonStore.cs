using Microsoft.AspNetCore.Authentication;
using SWAPI.Data;
using SWAPI.Data.Models;

namespace SWAPI.Local.Stores;

internal class PersonStore : BaseStorage
{
	#region Internal Properties

	private static IDictionary<int, Person>? _people;
	internal static IDictionary<int, Person> People
	{
		get => _people is null ? new Dictionary<int, Person>() : _people;
		private set => _people = value;
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
		if( _people is not null ) { return; }

		ICollection<Person> list = Factory.LoadPeople( GetIISPath( environment ) );
		string rootUrl = GetRootUrl( _resourceUrl );
		People = new Dictionary<int, Person>();

		foreach( var item in list )
		{
			int? key = GetItemKey( item.Url );
			if( key.HasValue && !People.ContainsKey( key.Value ) )
			{
				item.Url = rootUrl + item.Url;
				item.Homeworld = !string.IsNullOrWhiteSpace( item.Homeworld ) ?
					rootUrl + item.Homeworld : string.Empty;
				ProcessArray( item.Films as List<string>, rootUrl );
				ProcessArray( item.Species as List<string>, rootUrl );
				ProcessArray( item.Starships as List<string>, rootUrl );
				ProcessArray( item.Vehicles as List<string>, rootUrl );
				People.Add( key.Value, item );
			}
		}
		return;
	}

	#endregion
}
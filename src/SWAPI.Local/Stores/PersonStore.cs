using SWAPI.Data;
using SWAPI.Data.Models;

namespace SWAPI.Local.Stores;

internal class PersonStore : BaseStorage
{
	private static IDictionary<int, Person>? _people;
	private static string? _resourceUrl;
	private static readonly object _peopleLock = new();
	private static bool _peopleLoad = false;

	#region Internal Properties

	internal static IDictionary<int, Person> People
	{
		get => _people is null ? new Dictionary<int, Person>() : _people;
		private set => _people = value;
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
		if( _people is not null ) { return; }
		try
		{
			_resourceUrl ??= GetResourceUrl( request );
			string rootUrl = GetRootUrl( _resourceUrl );

			// Load people data
			Monitor.Enter( _peopleLock, ref _peopleLoad );
			ICollection<Person> list = Factory.LoadPeople( GetIISPath( environment ) );
			People = new Dictionary<int, Person>();
			foreach( var item in list )
			{
				int? key = GetItemKey( item.Url );
				if( key.HasValue && !People.ContainsKey( key.Value ) )
				{
					item.Url = SetUrlPrefix( rootUrl, item.Url );
					item.Homeworld = SetUrlPrefix( rootUrl, item.Homeworld );
					ProcessArray( item.Films as List<string>, rootUrl );
					ProcessArray( item.Species as List<string>, rootUrl );
					ProcessArray( item.Starships as List<string>, rootUrl );
					ProcessArray( item.Vehicles as List<string>, rootUrl );
					People.Add( key.Value, item );
				}
			}
		}
		catch( Exception ) { }
		finally
		{
			if( _peopleLoad )
			{
				Monitor.Exit( _peopleLock );
				_peopleLoad = false;
			}
		}


		return;
	}

	#endregion
}
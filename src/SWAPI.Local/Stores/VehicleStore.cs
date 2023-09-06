using SWAPI.Data;
using SWAPI.Data.Models;

namespace SWAPI.Local.Stores;

internal class VehicleStore : BaseStorage
{
	#region Internal Properties

	private static IDictionary<int, Vehicle>? _vehicles;
	internal static IDictionary<int, Vehicle> Vehicles
	{
		get => _vehicles is null ? new Dictionary<int, Vehicle>() : _vehicles;
		private set => _vehicles = value;
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
		if( _vehicles is not null ) { return; }

		ICollection<Vehicle> list = Factory.LoadVehicles( GetIISPath( environment ) );
		string rootUrl = GetRootUrl( _resourceUrl );
		Vehicles = new Dictionary<int, Vehicle>();

		foreach( var item in list )
		{
			int? key = GetItemKey( item.Url );
			if( key.HasValue && !Vehicles.ContainsKey( key.Value ) )
			{
				item.Url = rootUrl + item.Url;
				ProcessArray( item.Films as List<string>, rootUrl );
				ProcessArray( item.Pilots as List<string>, rootUrl );
				Vehicles.Add( key.Value, item );
			}
		}
		return;
	}

	#endregion
}
using SWAPI.Data;
using SWAPI.Data.Models;

namespace SWAPI.Local.Stores;

internal class VehicleStore : BaseStorage
{
	private static IDictionary<int, Vehicle>? _vehicles;
	private static string? _resourceUrl;
	private static readonly object _vehiclesLock = new();
	private static bool _vehiclesLoad = false;

	#region Internal Properties

	internal static IDictionary<int, Vehicle> Vehicles
	{
		get => _vehicles is null ? new Dictionary<int, Vehicle>() : _vehicles;
		private set => _vehicles = value;
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
		if( _vehicles is not null ) { return; }
		try
		{
			_resourceUrl ??= GetResourceUrl( request );
			string rootUrl = GetRootUrl( _resourceUrl );

			// Load vehicles data
			Monitor.Enter( _vehiclesLock, ref _vehiclesLoad );
			ICollection<Vehicle> list = Factory.LoadVehicles( GetIISPath( environment ) );
			Vehicles = new Dictionary<int, Vehicle>();
			foreach( var item in list )
			{
				int? key = GetItemKey( item.Url );
				if( key.HasValue && !Vehicles.ContainsKey( key.Value ) )
				{
					item.Url = SetUrlPrefix( rootUrl, item.Url );
					ProcessArray( item.Films as List<string>, rootUrl );
					ProcessArray( item.Pilots as List<string>, rootUrl );
					Vehicles.Add( key.Value, item );
				}
			}
		}
		catch( Exception ) { }
		finally
		{
			if( _vehiclesLoad )
			{
				Monitor.Exit( _vehiclesLock );
				_vehiclesLoad = false;
			}
		}
		return;
	}

	#endregion
}
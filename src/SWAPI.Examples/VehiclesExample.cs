using SWAPI.Client;
using SWAPI.Data.Models;

namespace SWAPI.Examples;

/// <summary>Demonstrates the retrieving of films that a Vehicle appears in.</summary>
public class VehiclesExample : IExecutor
{
	/// <inheritdoc/>
	public void Execute()
	{
		bool retrieveAll = false;
		IRepository<Vehicle> vehicleRepository = new Repository<Vehicle>();

		if( retrieveAll )
		{
			ICollection<Vehicle>? collection = vehicleRepository.GetEntities( size: int.MaxValue );
			if( collection is null )
			{
				Console.WriteLine( "No vehicles!" );
			}
			return;
		}

		int vehicleId = 8;
		Vehicle? vehicle = vehicleRepository.GetById( vehicleId );

		IRepository<Film> filmRepository = new Repository<Film>();
		if( vehicle is not null && vehicle.Films?.Count > 0 )
		{
			Console.WriteLine( "Vehicle {0} has {1} films:", vehicle.Name, vehicle.Films.Count );
			foreach( var film in vehicle.Films )
			{
				int filmId = GetFilmId( film );
				Film? relatedFilm = filmRepository.GetById( filmId );
				Console.WriteLine( relatedFilm?.Title );
			}
		}
	}

	private static int GetFilmId( string filmUrl )
	{
		if( filmUrl.EndsWith( '/' ) ) { filmUrl = filmUrl[..^1]; }
		int last = filmUrl.LastIndexOf( "/" );
		string id = filmUrl.Substring( last + 1, filmUrl.Length - last - 1 );

		int result = int.Parse( id );
		return result;
	}
}
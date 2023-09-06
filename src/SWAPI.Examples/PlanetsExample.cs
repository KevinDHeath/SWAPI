using SWAPI.Client;
using SWAPI.Data.Models;

namespace SWAPI.Examples;

/// <summary>Demonstrates the use of page and size when retrieving entities.</summary>
public class PlanetsExample : IExecutor
{
	/// <inheritdoc/>
	public void Execute()
	{
		bool retrieveAll = false;
		ICollection<Planet>? collection;
		IRepository<Planet> planetsRepository = new Repository<Planet>();

		if( retrieveAll )
		{
			collection = planetsRepository.GetEntities( size: int.MaxValue );
			if( collection is null )
			{
				Console.WriteLine( "No planets!" );
			}
			return;
		}

		int page = 2;
		int size = 5;
		collection = planetsRepository.GetEntities( page, size );

		if( collection is null )
		{
			Console.WriteLine( "There are no planets on page {0}", page );
			return;
		}

		int index = 1;
		foreach( var planet in collection )
		{
			Console.WriteLine( index + ". Name: " + planet.Name );
			Console.WriteLine( "   Terrain: " + planet.Terrain );
			index++;
		}
	}
}
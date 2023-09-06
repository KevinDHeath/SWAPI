// Ignore Spelling: Starships

using SWAPI.Client;
using SWAPI.Data.Models;

namespace SWAPI.Examples;

/// <summary>Demonstrates the handling of an non-existent resource Id.</summary>
public class StarshipsExample : IExecutor
{
	/// <inheritdoc/>
	public void Execute()
	{
		bool retrieveAll = false;
		IRepository<Starship> starshipRepository = new Repository<Starship>();

		if( retrieveAll )
		{
			ICollection<Starship>? collection = starshipRepository.GetEntities( size: int.MaxValue );
			if( collection is null )
			{
				Console.WriteLine( "No starships!" );
			}
			return;
		}

		int starshipId = 3;
		int nonExistingId = 1;

		Starship? starshipDetails = starshipRepository.GetById( starshipId );
		Starship? anotherStarship = starshipRepository.GetById( nonExistingId );

		PrintStarshipName( starshipDetails, starshipId );
		PrintStarshipName( anotherStarship, nonExistingId );
	}

	private static void PrintStarshipName( Starship? starship, int starshipID )
	{
		if( starship is not null )
		{
			Console.WriteLine( starship.Name );
		}
		else
		{
			Console.WriteLine( "Cannot find starship with id: " + starshipID );
		}
	}
}
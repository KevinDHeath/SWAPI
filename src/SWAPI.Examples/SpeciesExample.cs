using SWAPI.Client;
using SWAPI.Data.Models;

namespace SWAPI.Examples;

/// <summary>Demonstrates the handling of the 'unknown' value.</summary>
public class SpeciesExample : IExecutor
{
	/// <inheritdoc/>
	public void Execute()
	{
		bool retrieveAll = false;
		IRepository<Species> speciesRepository = new Repository<Species>();

		if( retrieveAll )
		{
			ICollection<Species>? collection = speciesRepository.GetEntities( size: int.MaxValue );
			if( collection is null )
			{
				Console.WriteLine( "No species!" );
			}
			return;
		}

		Species? item = speciesRepository.GetById( 5 );
		const int SpecialSpan = 2;

		if( item != null && item.AverageLifespan != null && item.AverageLifespan != Program.Unknown )
		{
			int lifeSpan = int.Parse( item.AverageLifespan );
			Console.WriteLine( item.Name + " life span: " + lifeSpan );
		}
		else if( int.TryParse( item?.AverageLifespan, out int lifeSpanAverage ) )
		{
			Console.WriteLine( item.Name + " life span: " + ( lifeSpanAverage + SpecialSpan ) );
		}
		else
		{
			Console.WriteLine( item?.Name + " life span: " + item?.AverageLifespan );
		}
	}
}
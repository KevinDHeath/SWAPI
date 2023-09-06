using SWAPI.Client;
using SWAPI.Data.Models;

namespace SWAPI.Examples;

/// <summary>Demonstrates the use of an extension to the Person entity.</summary>
public class PeopleExample : IExecutor
{
	/// <inheritdoc/>
	public void Execute()
	{
		bool retrieveAll = false;

		if( retrieveAll )
		{
			IRepository<Person> personRepo = new Repository<Person>();
			ICollection<Person>? collection = personRepo.GetEntities( size: int.MaxValue );
			if( collection is null )
			{
				Console.WriteLine( "No people!" );
			}
			return;
		}

		IRepository<PersonEx> personExRepo = new Repository<PersonEx>();
		PersonEx? kenobi = personExRepo.GetById( 10 );

		if( kenobi is not null )
		{
			Console.WriteLine( kenobi.ToString() );
		}
		else
		{
			Console.WriteLine( "Cannot find this person!" );
		}
	}
}
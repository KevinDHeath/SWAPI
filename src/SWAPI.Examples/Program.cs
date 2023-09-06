// Ignore Spelling: Starships

namespace SWAPI.Examples;

internal class Program
{
	internal const string Unknown = "unknown";

	static void Main()
	{
		ProcessExecuteCommand( new ServiceDemo.FilmsFromFileDemo(), "Other Service" );

		//ProcessExecuteCommand( new FilmsExample(), "Films" );
		//ProcessExecuteCommand( new PeopleExample(), "People" );
		//ProcessExecuteCommand( new PlanetsExample(), "Planets" );
		//ProcessExecuteCommand( new SpeciesExample(), "Species" );
		//ProcessExecuteCommand( new StarshipsExample(), @"Starships" );
		//ProcessExecuteCommand( new VehiclesExample(), "Vehicles" );
	}

	private static void ProcessExecuteCommand( IExecutor executor, string typeName )
	{
		ConsoleColor originalColor = Console.ForegroundColor;
		Console.ForegroundColor = ConsoleColor.Cyan;
		Console.WriteLine( "Starting {0} example", typeName );
		Console.ForegroundColor = originalColor;

		Console.WriteLine();
		executor.Execute();
		Console.WriteLine();

		Console.WriteLine( "Press [Enter] to continue" );
		Console.ReadLine();
	}
}
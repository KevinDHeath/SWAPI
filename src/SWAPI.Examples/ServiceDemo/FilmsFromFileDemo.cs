using System.Reflection;
using SWAPI.Client;
using SWAPI.Data.Models;

namespace SWAPI.Examples.ServiceDemo;

/// <summary>Demonstrates the retrieving from local files of films that a Star Ship appears in.</summary>
public class FilmsFromFileDemo : IExecutor
{
	/// <inheritdoc/>
	public void Execute()
	{
		var path = Assembly.GetExecutingAssembly().Location;
		path = Path.GetDirectoryName( path );
		if( path is null ) { return; }

		DirectoryInfo? directory = new( path );
		while( directory is not null && !directory.FullName.EndsWith( "SWAPI" ) )
		{
			directory = directory.Parent;
		}

		if( directory is null || directory.FullName is null ) { return; }

		path = $"{directory.FullName}\\";
		var service = new JsonFileService( path );

		IRepository<Film> filmsRepository = new Repository<Film>( service );
		List<Film>? filmsFromFile = filmsRepository.GetEntities()?.ToList();
		if( filmsFromFile is null ) { return; }

		Repository<Starship> starshipsRepo = new( service );

		for( int i = 0; i < filmsFromFile.Count; i++ )
		{
			Console.WriteLine( i + 1 + ". " + filmsFromFile[i].Title );
			if( filmsFromFile[i].Starships is not null && filmsFromFile[i].Starships?.Count > 0 )
			{
				foreach( string starshipUrl in filmsFromFile[i].Starships! )
				{
					int id = GetFilmId( starshipUrl );
					Starship? starship = starshipsRepo.GetById( id );
					if( starship is not null )
					{
						Console.WriteLine( "\t" + starship.Name );
					}
					else
					{
						Console.WriteLine( "\tNot found with id {0}!", id );
					}
				}
			}
		}
	}

	private static int GetFilmId( string filmUrl )
	{
		int secondSlash = filmUrl.LastIndexOf( "/" );
		int firstSlash = filmUrl.LastIndexOf( "/", secondSlash - 1 );
		int lengthOfSubstring = ( secondSlash - firstSlash ) - 1;
		string stringId = filmUrl.Substring( firstSlash + 1, lengthOfSubstring );

		int result = int.Parse( stringId );
		return result;
	}
}
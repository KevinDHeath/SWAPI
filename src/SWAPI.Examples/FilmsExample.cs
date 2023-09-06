using SWAPI.Client;
using SWAPI.Data.Models;

namespace SWAPI.Examples;

/// <summary>Demonstrates the retrieving of all films.</summary>
public class FilmsExample : IExecutor
{
	/// <inheritdoc/>
	public void Execute()
	{
		IRepository<Film> filmsRepo = new Repository<Film>();
		ICollection<Film>? films = filmsRepo.GetEntities( size: int.MaxValue );
		if( films is null )
		{
			Console.WriteLine( "No films!" );
			return;
		}

		foreach( Film film in films )
		{
			Console.WriteLine( new string( '#', 25 ) );
			Console.WriteLine( "Title: " + film.Title );
			Console.WriteLine( "Opening crawl: " + film.OpeningCrawl );
			Console.WriteLine( "Director: " + film.Director );
			Console.WriteLine( "Release date: " + film.ReleaseDate );
			Console.WriteLine( "Episode:  " + film.EpisodeId );
			Console.WriteLine( "Producer: " + film.Producer );
			Console.WriteLine( "Characters: " + film.Characters?.Count );
			Console.WriteLine( "Planets: " + film.Planets?.Count + " :" );

			if( film.Planets is not null )
			{
				foreach( var item in film.Planets )
				{
					Console.WriteLine( new string( '-', 3 ) + ">" + item );
				}
			}

			Console.WriteLine( "URL: " + film.Url );
			Console.WriteLine( "Date edited: " + film.Edited );
			Console.WriteLine( "Date created: " + film.Created );
		}
	}
}
// Ignore Spelling: Starships

using System.Reflection;
using System.Text;
using System.Text.Json;
using SWAPI.Data.Models;

namespace SWAPI.Data;

/// <summary>Populates the resources from JSON files.</summary>
public static class Factory
{
	#region Public Methods

	private static ICollection<Film>? _films;
	/// <summary>Loads the collection of film resources.</summary>
	/// <param name="path">Optional path to the file location.</param>
	/// <returns>A empty list is returned if the data file could not be located.</returns>
	public static ICollection<Film> LoadFilms( string path = "" )
	{
		if( _films is null )
		{
			var res = LoadDataFile<Film>( "allfilms.json", path );
			_films = res is not null ? res : new List<Film>();
		}

		return _films;
	}

	private static ICollection<Person>? _people;
	/// <summary>Loads the collection of person resources.</summary>
	/// <param name="path">Optional path to the file location.</param>
	/// <returns>A empty list is returned if the data file could not be located.</returns>
	public static ICollection<Person> LoadPeople( string path = "" )
	{
		if( _people is null )
		{
			var res = LoadDataFile<Person>( "allpeople.json", path );
			_people = res is not null ? res : new List<Person>();
		}
		return _people;
	}

	private static ICollection<Planet>? _planets;
	/// <summary>Loads the collection of planet resources.</summary>
	/// <param name="path">Optional path to the file location.</param>
	/// <returns>A empty list is returned if the data file could not be located.</returns>
	public static ICollection<Planet> LoadPlanets( string path = "" )
	{
		if( _planets is null )
		{
			var res = LoadDataFile<Planet>( "allplanets.json", path );
			_planets = res is not null ? res : new List<Planet>();
		}
		return _planets;
	}

	private static ICollection<Species>? _species;
	/// <summary>Loads the collection of species resources.</summary>
	/// <param name="path">Optional path to the file location.</param>
	/// <returns>A empty list is returned if the data file could not be located.</returns>
	public static ICollection<Species> LoadSpecies( string path = "" )
	{
		if( _species is null )
		{
			var res = LoadDataFile<Species>( "allspecies.json", path );
			_species = res is not null ? res : new List<Species>();
		}
		return _species;
	}

	private static ICollection<Starship>? _starships;
	/// <summary>Loads the collection of star-ship resources.</summary>
	/// <param name="path">Optional path to the file location.</param>
	/// <returns>A empty list is returned if the data file could not be located.</returns>
	public static ICollection<Starship> LoadStarships( string path = "" )
	{
		if( _starships is null )
		{
			var res = LoadDataFile<Starship>( "allstarships.json", path );
			_starships = res is not null ? res : new List<Starship>();
		}
		return _starships;
	}

	private static ICollection<Vehicle>? _vehicles;
	/// <summary>Loads the collection of vehicle resources.</summary>
	/// <param name="path">Optional path to the file location.</param>
	/// <returns>A empty list is returned if the data file could not be located.</returns>
	public static ICollection<Vehicle> LoadVehicles( string path = "" )
	{
		if( _vehicles is null )
		{
			var res = LoadDataFile<Vehicle>( "allvehicles.json", path );
			_vehicles = res is not null ? res : new List<Vehicle>();
		}
		return _vehicles;
	}

	#endregion

	#region Private Methods

	/// <summary>Gets the collection of data objects.</summary>
	/// <param name="file">Name of the JSON data file.</param>
	/// <param name="path">Directory name where the JSON data file is located.</param>
	/// <returns>An empty collection is returned if the JSON file could not be loaded.</returns>
	private static ICollection<T> LoadDataFile<T>( string file, string path ) where T : BaseModel
	{
		try
		{
			// Determine the file location
			if( !File.Exists( Path.Combine( path, file ) ) ) { path = string.Empty; }
			path = string.IsNullOrWhiteSpace( path ) ? GetFileLocation() : path;
			file = Path.Combine( path, file );

			if( !string.IsNullOrWhiteSpace( path ) && File.Exists( file ) )
			{
				// Read the JSON file
				string? json = null;
				StringBuilder sb = new( "[" );
				using StreamReader reader = new( file );
				while( !reader.EndOfStream )
				{
					string? line = reader.ReadLine();

					if( line is not null && line.StartsWith( "{" ) )
					{
						if( !line.StartsWith( "{\"count\"" ) )
						{
							line = line.Replace( "https://swapi.dev/api/", string.Empty );
							if( !line.EndsWith( ',' ) ) { line += ','; }
							_ = sb.Append( line );
						}
					}
				}
				json = sb.ToString();

				if( json is not null )
				{
					// Deserialize the JSON to a collection
					json = json[..^1] + "]";
					var obj = JsonSerializer.Deserialize<ICollection<T>>( json );
					if( obj is not null ) { return obj; }
				}
			}
		}
		catch( Exception ) { }

		return new List<T>();
	}

	private static string? _filelocation;
	/// <summary>Gets the data file location when running in debug mode.</summary>
	/// <returns>An empty string is returned if the location could not be determined.</returns>
	private static string GetFileLocation()
	{
		if( _filelocation is null )
		{
			const string cError = "";
			const StringComparison cCompare = StringComparison.InvariantCultureIgnoreCase;

			// Get the folder name where the local JSON files are stored (SWAPI\data)
			string? name = Assembly.GetExecutingAssembly().GetName().Name?.Split( '.' )[0];
			if( name is null ) { return cError; }

			// Find the folder where the JSON files are stored
			name = name.ToLowerInvariant();
			string? path = Assembly.GetExecutingAssembly().Location;
			try
			{
				path = Path.GetDirectoryName( path );
				if( path is null ) { return cError; }

				DirectoryInfo? directory = new( path );
				while( directory is not null && !directory.FullName.EndsWith( name, cCompare ) )
				{
					directory = directory.Parent;
				}

				if( directory is null || directory.FullName is null ) { return cError; }
				_filelocation = $"{directory.FullName}\\data\\";
			}
			catch( Exception ) { return cError; }
		}

		return _filelocation;
	}

	#endregion
}
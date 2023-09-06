using SWAPI.Client;

namespace SWAPI.Examples.ServiceDemo;

/// <summary>This service is for consuming data from local JSON files.</summary>
public class JsonFileService : IDataService
{
	#region Constructor and Variables

	private readonly string _filePath;

	/// <summary>Initializes a new instance of the JsonFileService class.</summary>
	/// <param name="filePath">Location of the JSON data files.</param>
	public JsonFileService( string filePath )
	{
		_filePath = filePath;
	}

	#endregion

	#region IDataService Implementation

	/// <inheritdoc/>
	public string? RetrieveData( string url )
	{
		return IsSingleUrl( url ) ? GetSingle( _filePath, url ) : GetMany( _filePath );
	}

	#endregion

	#region Private Methods

	private static string GetMany( string filePath )
	{
		filePath += "allfilms.json";
		string result = string.Empty;

		using( StreamReader reader = new( filePath ) )
		{
			result = reader.ReadToEnd();
		}

		return result;
	}

	private static string? GetSingle( string filePath, string url )
	{
		filePath += "allstarships.json";
		int index = url.IndexOf( "/" );
		string id = url[( index )..];
		id += "/";

		using StreamReader reader = new( filePath );
		while( !reader.EndOfStream )
		{
			string? line = reader.ReadLine();

			if( line is not null && line.StartsWith( "{" ) )
			{
				int pos = line.IndexOf( "\"url\"" );
				if( pos >= 0 )
				{
					if( line.IndexOf( id, pos + 4 ) >= 0 )
					{
						if( line.EndsWith( "," ) ) { line = line[..^1]; }
						return line;
					}
				}
			}
		}
		return null;
	}

	private static bool IsSingleUrl( string urlToCheck )
	{
		return !urlToCheck.Contains( '?' );
	}

	#endregion
}
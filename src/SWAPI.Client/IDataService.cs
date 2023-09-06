namespace SWAPI.Client;

/// <summary>A service for consuming data from a remote REST API.</summary>
public interface IDataService
{
	/// <summary>Retrieves the data.</summary>
	/// <param name="url">The URL of the REST API.</param>
	/// <returns>Data as a string, null if there were errors while processing the request.</returns>
	string? RetrieveData( string url );
}
using System.Net.Http.Headers;

namespace SWAPI.Client;

/// <summary>This is the default service for consuming data from SWAPI.</summary>
public class DefaultDataService : IDataService
{
	private const string cUrl = "https://swapi.dev/api/"; // The default SWAPI URL
	private static readonly HttpClient _httpClient = CreateSharedClient();

	/// <summary>Initializes a new instance of the DefaultDataService class.</summary>
	public DefaultDataService()
	{ }

	#region IDataService Implementation

	/// <inheritdoc/>
	public string? RetrieveData( string url )
	{
		if( !url.ToLowerInvariant().StartsWith( @"http" ) )
		{
			url = _httpClient.BaseAddress?.OriginalString + url;
		}
		try
		{
			HttpRequestMessage clientRequest = new( HttpMethod.Get, url );
			return GetResponseDataAsync( clientRequest ).Result;
		}
		catch( HttpRequestException ex )
		{
			// Synchronous exception
			System.Diagnostics.Debug.WriteLine( ex );
			return null;
		}
		catch( AggregateException ex )
		{
			// Asynchronous exception
			System.Diagnostics.Debug.WriteLine( ex );
			return null;
		}
	}

	#endregion

	#region Private Methods

	private static HttpClient CreateSharedClient()
	{
		SocketsHttpHandler handler = new()
		{
			PooledConnectionLifetime = TimeSpan.FromMinutes( 15 ) // Recreate every 15 minutes
		};

		HttpClient rtn = new( handler )
		{
			BaseAddress = new Uri( cUrl )
		};

		rtn.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );

		return rtn;
	}

	private static async Task<string> GetResponseDataAsync( HttpRequestMessage clientRequest )
	{
		HttpResponseMessage clientResponse = await _httpClient.SendAsync( clientRequest );
		_ = clientResponse.EnsureSuccessStatusCode();

		using StreamReader clientReader = new( clientResponse.Content.ReadAsStream() );
		return await clientReader.ReadToEndAsync();
	}

	#endregion
}
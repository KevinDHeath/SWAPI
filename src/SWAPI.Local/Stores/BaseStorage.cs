using SWAPI.Data.Models;

namespace SWAPI.Local.Stores;

internal abstract class BaseStorage
{
	private static string? _iisPath;
	private static string? _rootUrl;

	#region Protected Methods

	/// <summary>Gets the path to the JSON data files when running in IIS.</summary>
	/// <param name="environment">The web hosting environment the application is running in.</param>
	/// <returns>An empty string is returned if the path could not be determined.</returns>
	protected static string GetIISPath( IWebHostEnvironment environment )
	{
		if( _iisPath is not null ) { return _iisPath; }

		var path = environment.ContentRootPath;
		try
		{
			_iisPath = Path.Combine( path, @"App_Data" );
			return _iisPath;
		}
		catch( Exception ) { }
		return string.Empty;
	}

	/// <summary>Gets the API URL from a resource URL.</summary>
	/// <param name="url">The resource URL.</param>
	/// <returns>An empty string is returned if the URL could not be determined.</returns>
	protected static string GetRootUrl( string url )
	{
		if( _rootUrl is not null ) { return _rootUrl; }

		_rootUrl = url.LastIndexOf( '/' ) > 0 ? url[..url.LastIndexOf( '/' )] : url;
		if( !_rootUrl.EndsWith( '/' ) ) { _rootUrl += '/'; }
		return _rootUrl;
	}

	/// <summary>Gets the resource URL from a HTTP request.</summary>
	/// <param name="request">The HTTP request.</param>
	/// <returns>The URL of the resource.</returns>
	protected static string GetResourceUrl( HttpRequest request )
	{
		// Remove specific item in URL
		string path = request.Path;
		if( path.EndsWith( '/' ) ) { path = path[..^1]; }
		int index = path.LastIndexOf( '/' );
		if( int.TryParse( path.AsSpan( index + 1, path.Length - index - 1 ), out _ ) ) { path = path[..index]; }

		return $"{request.Scheme}://{request.Host}{request.PathBase}{path}";
	}

	protected static int? GetItemKey( string? path )
	{
		if( !string.IsNullOrWhiteSpace( path ) )
		{
			if( path.EndsWith( '/' ) ) { path = path[..^1]; }
			int index = path.LastIndexOf( '/' );
			if( int.TryParse( path.AsSpan( index + 1, path.Length - index - 1 ), out int key ) )
			{
				return key;
			}
		}
		return null;
	}

	protected static void ProcessArray( List<string>? list, string prefix )
	{
		if( list is null ) { return; }
		for( int index = 0; index < list.Count; index++ )
		{
			if( list[index] is not null ) { list[index] = prefix + list[index]; }
		}
	}

	#endregion

	private const int cPageSize = 10;

	#region Internal Methods

	internal static Collection<T> DoPaging<T>( int page, string search, ICollection<T> results,
		string url ) where T : BaseModel
	{
		Collection<T> rtn = new() { Count = results.Count };
		if( results.Count > cPageSize )
		{
			int start = 0;
			int end = cPageSize;
			if( page > 1 )
			{
				start = ( page - 1 ) * cPageSize;
				end = start + cPageSize;
				if( start <= results.Count )
				{
					rtn.Previous = url + "/?page=" + ( page - 1 ).ToString();
					if( search.Length > 0 ) { rtn.Previous += $"&search={search}"; }
				}
			}
			if( end < results.Count )
			{
				rtn.Next = url + "/?page=" + ( page + 1 ).ToString();
				if( search.Length > 0 ) { rtn.Next += $"&search={search}"; }
			}

			results = results.Take( new Range( start, end ) ).ToList();
		}

		rtn.Results = results;

		return rtn;
	}

	#endregion
}
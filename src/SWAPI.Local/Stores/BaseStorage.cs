using SWAPI.Data.Models;

namespace SWAPI.Local.Stores;

internal abstract class BaseStorage
{
	protected const char cSep = '/';
	private const int cPageSize = 10;

	private static string? _iisPath;
	private static readonly object _iisPathLock = new();
	private static bool _iisPathLoad = false;

	private static string? _rootUrl;
	private static readonly object _rootUrlLock = new();
	private static bool _rootUrlLoad = false;

	#region Protected Methods

	/// <summary>Gets the path to the JSON data files when running in IIS.</summary>
	/// <param name="environment">The web hosting environment the application is running in.</param>
	/// <returns>An empty string is returned if the path could not be determined.</returns>
	protected static string GetIISPath( IWebHostEnvironment environment )
	{
		if( _iisPath is not null ) { return _iisPath; }
		try
		{
			// Store the data path
			Monitor.Enter( _iisPathLock, ref _iisPathLoad );
			var path = environment.ContentRootPath;
			_iisPath = Path.Combine( path, @"App_Data" );
			return _iisPath;
		}
		catch( Exception ) { }
		finally
		{
			if( _iisPathLoad )
			{
				Monitor.Exit( _iisPathLock );
				_iisPathLoad = false;
			}
		}
		return _iisPath is not null ? _iisPath : string.Empty;
	}

	/// <summary>Gets the API URL from a resource URL.</summary>
	/// <param name="url">The resource URL.</param>
	/// <returns>An empty string is returned if the URL could not be determined.</returns>
	protected static string GetRootUrl( string url )
	{
		if( _rootUrl is not null ) { return _rootUrl; }
		try
		{
			// Store the root URL
			Monitor.Enter( _rootUrlLock, ref _rootUrlLoad );
			_rootUrl = url.LastIndexOf( cSep ) > 0 ? url[..url.LastIndexOf( cSep )] : url;
			if( !_rootUrl.EndsWith( cSep ) ) { _rootUrl += cSep; }
		}
		catch( Exception ) { }
		finally
		{
			if( _rootUrlLoad )
			{
				Monitor.Exit( _rootUrlLock );
				_rootUrlLoad = false;
			}
		}

		return _rootUrl is not null ? _rootUrl : string.Empty;
	}

	/// <summary>Gets the resource URL from a HTTP request.</summary>
	/// <param name="request">The HTTP request.</param>
	/// <returns>An empty string is returned if the URL could not be determined.</returns>
	protected static string GetResourceUrl( HttpRequest? request )
	{
		if( request is not null )
		{
			// Remove specific item in URL
			string path = request.Path;
			if( path.EndsWith( cSep ) ) { path = path[..^1]; }
			int index = path.LastIndexOf( cSep );
			if( int.TryParse( path.AsSpan( index + 1, path.Length - index - 1 ), out _ ) )
			{ path = path[..index]; }

			return $"{request.Scheme}://{request.Host}{request.PathBase}{path}";
		}
		return string.Empty;
	}

	protected static int? GetItemKey( string? path )
	{
		if( !string.IsNullOrWhiteSpace( path ) )
		{
			if( path.EndsWith( cSep ) ) { path = path[..^1]; }
			int index = path.LastIndexOf( cSep );
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
			list[index] = SetUrlPrefix( prefix, list[index] );
		}
	}

	protected static string SetUrlPrefix( string prefix, string? val )
	{
		if( string.IsNullOrWhiteSpace( val ) ) { return string.Empty; }
		if( val.ToLowerInvariant().StartsWith( Uri.UriSchemeHttp ) ) { return val; }
		return prefix + val;
	}

	#endregion

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
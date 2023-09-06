using System.Text.Json;
using SWAPI.Data.Models;

namespace SWAPI.Client;

/// <summary>Class that stores entities to work with.</summary>
/// <typeparam name="T">Generic object that inherits BaseModel.</typeparam>
public class Repository<T> : IRepository<T> where T : BaseModel
{
	#region Constructors and Variables

	private readonly IDataService _dataService;
	private readonly T _entityType;

	/// <summary>Initializes a new instance of the Repository class.</summary>
	/// <remarks>Uses the default data service to retrieve the data.</remarks>
	public Repository() : this( new DefaultDataService() )
	{ }

	/// <summary>Initializes a new instance of the Repository class.</summary>
	/// <param name="dataService">The data service to retrieve the data.</param>
	public Repository( IDataService dataService )
	{
		_entityType = EntityInitializer<T>.Instance();
		_dataService = dataService;
	}

	#endregion

	#region IRepository<T> Implementation

	/// <inheritdoc/>
	public T? GetById( int id )
	{
		string url = $"{_entityType.GetPath()}{id}";
		string? jsonResponse = _dataService.RetrieveData( url );
		return jsonResponse is null ? null : JsonSerializer.Deserialize<T>( jsonResponse );
	}

	/// <summary>Gets a collection of entities.</summary>
	/// <param name="page">Page number. Must be greater than 0.</param>
	/// <param name="size">Number of entities to include. Must be greater than 0</param>
	/// <returns>An ICollection&lt;T&gt; of entities that inherits BaseEntity.</returns>
	/// <remarks>If calling this method via the interface the page defaults to 1, and the size defaults to 10.</remarks>
	public ICollection<T>? GetEntities( int page, int size )
	{
		if( page <= 0 || size <= 0 ) { return null; }

		Collection<T>? collection = new()
		{
			Next = _entityType.GetPath() + "?page=" + page,
			Previous = null,
		};

		IEnumerable<T>? results = new List<T>();
		string? jsonResponse;
		while( collection.Next is not null )
		{
			jsonResponse = _dataService.RetrieveData( collection.Next );
			if( jsonResponse == null ) { return null; }

			collection = JsonSerializer.Deserialize<Collection<T>>( jsonResponse );
			if( collection is null ) { return null; }

			if( collection.Results is not null )
			{
				results = results.Union( collection.Results );
				if( results.Count() >= size )
				{
					return results.Take( size ).ToList();
				}
			}
		}
		return results.ToList();
	}

	#endregion
}
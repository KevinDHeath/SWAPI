using SWAPI.Data.Models;

namespace SWAPI.Client;

/// <summary>Data repository interface.</summary>
/// <typeparam name="T">Generic object that inherits BaseModely.</typeparam>
public interface IRepository<T> where T : BaseModel
{
	/// <summary>Gets an entity by it's resource identifier.</summary>
	/// <param name="id">The resource identifier.</param>
	/// <returns>An entity that inherits BaseModel.</returns>
	T? GetById( int id );

	/// <summary>Gets a collection of entities.</summary>
	/// <param name="page">Page number. The default is 1.</param>
	/// <param name="size">Number of entities to include. The default is 10.</param>
	/// <returns>An ICollection&lt;T&gt; of entities that inherits BaseModel.</returns>
	ICollection<T>? GetEntities( int page = 1, int size = 10 );
}
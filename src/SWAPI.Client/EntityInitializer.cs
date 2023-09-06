using System.Linq.Expressions;
using SWAPI.Data.Models;

namespace SWAPI.Client;

/// <summary>Static helper initializer.</summary>
/// <typeparam name="T">Generic object that inherits BaseModel.</typeparam>
/// <remarks>This static helper initializer is 3x faster than System.Activator.CreateInstance&lt;T&gt;</remarks>
internal static class EntityInitializer<T> where T : BaseModel
{
	/// <summary>Gets an instance of &lt;T&gt;.</summary>
	public static readonly Func<T> Instance = Expression.Lambda<Func<T>>( Expression.New( typeof( T ) ) ).Compile();
}
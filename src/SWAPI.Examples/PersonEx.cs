using SWAPI.Data.Models;

namespace SWAPI.Examples;

/// <summary>Person entity extension.</summary>
public class PersonEx : Person
{
	/// <inheritdoc/>
	public override string ToString()
	{
		return Name + Environment.NewLine +
			"Birth year: " + BirthYear + Environment.NewLine +
			"Has " + Starships?.Count + @" starships";
	}
}
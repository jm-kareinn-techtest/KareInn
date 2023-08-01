using BeerDirectory.Core.Domain.Entities;

namespace BeerDirectory.Core.Repositories.Interfaces.Filters
{
	public interface IBeerFilter : IEntityFilter<Beer>
	{
		int? Style { get; set; }
		string? SearchTerms { get; set; }
	}
}
using BeerDirectory.Core.Domain.Entities;
using BeerDirectory.Core.Repositories.Interfaces.Filters;

namespace BeerDirectory.Core.Repositories.Interfaces
{
	public interface IBeerRepository : IEntityRepository<Beer, IBeerFilter>
	{
		
	}
}
using BeerDirectory.Core.Repositories.Interfaces.Filters;
using MongoDB.Driver;

namespace BeerDirectory.Core.Repositories.Filters
{
	public abstract class EntityFilter<T> : IEntityFilter<T>
	{
		public int Skip { get; set; }
		public int Take { get; set; }

		public abstract FilterDefinition<T> GetFilterDefinition();
	}
}
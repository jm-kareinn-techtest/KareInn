using BeerDirectory.Core.Domain.Entities;
using BeerDirectory.Core.Repositories.Interfaces.Filters;

namespace BeerDirectory.Core.Tests.Services.TestImplementations.Filters
{
	public class EntityFilter<T> : IEntityFilter<T>
		where T : Entity
	{
		public int Skip { get; set; }
		public int Take { get; set; }
	}
}
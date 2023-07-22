using BeerDirectory.Core.Domain.Entities;
using BeerDirectory.Core.Repositories.Interfaces.Filters;
using MongoDB.Driver;

namespace BeerDirectory.Core.Repositories.Filters
{
	public class BeerFilter : EntityFilter<Beer>, IBeerFilter
	{
		public override FilterDefinition<Beer> GetFilterDefinition()
		{
			var filter = Builders<Beer>.Filter.Empty;
			return filter;
		}
	}
}
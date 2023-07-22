using BeerDirectory.Core.Domain.Entities;
using BeerDirectory.Core.Repositories.Interfaces.Filters;

namespace BeerDirectory.Core.Tests.Services.TestImplementations.Filters
{
	public class BeerFilter : EntityFilter<Beer>, IBeerFilter
	{

	}
}
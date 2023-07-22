using System;
using System.Collections.Generic;
using BeerDirectory.Core.Domain.Entities;
using BeerDirectory.Core.Repositories.Interfaces;
using BeerDirectory.Core.Repositories.Interfaces.Filters;
using BeerDirectory.Core.Tests.Services.TestImplementations.Filters;

namespace BeerDirectory.Core.Tests.Services.TestImplementations
{
	public class FakeBeerRepository : FakeEntityRepository<Beer>, IBeerRepository
	{
		public FakeBeerRepository(List<Beer> entities, List<DateTime> saveTimestamps) : base(entities, saveTimestamps)
		{
		}

		public IEnumerable<Beer> Get(IBeerFilter filter)
		{
			return base.Get(filter as BeerFilter);
		}

		public new IBeerFilter GetFilter()
		{
			return new BeerFilter();
		}
	}
}
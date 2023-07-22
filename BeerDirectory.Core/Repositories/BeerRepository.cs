using System;
using System.Collections.Generic;
using BeerDirectory.Core.Domain.Entities;
using BeerDirectory.Core.Repositories.Client;
using BeerDirectory.Core.Repositories.Filters;
using BeerDirectory.Core.Repositories.Interfaces;
using BeerDirectory.Core.Repositories.Interfaces.Filters;
using MongoDB.Driver;

namespace BeerDirectory.Core.Repositories
{
	public class BeerRepository : EntityRepository<Beer, BeerFilter>, IBeerRepository
	{
		private readonly BeerDirectoryMongoClient _client;

		public BeerRepository(BeerDirectoryMongoClient client) : base(client)
		{
			_client = client;
		}

		public IEnumerable<Beer> Get(IBeerFilter filter)
		{
			return base.Get(filter as BeerFilter);
		}

		public new IBeerFilter GetFilter()
		{
			return base.GetFilter();
		}
	}
}
using System;
using BeerDirectory.Core.Domain.Entities;
using BeerDirectory.Core.Repositories;
using BeerDirectory.Core.Repositories.Client;
using BeerDirectory.Core.Repositories.Filters;

namespace BeerDirectory.Core.Tests.Repositories
{
	public class BeerRepositoryTests : EntityRepositoryTests<Beer, BeerFilter>
	{
		public BeerRepositoryTests()
		{
			// Connection string change required to make tests pass locally. 
			//_client = new BeerDirectoryMongoClient("mongodb://localhost:27017", "beer-integ-tests");
			_client = new BeerDirectoryMongoClient("mongodb://root:example@localhost:27017", "beer-integ-tests");
			_repository = new BeerRepository(_client);
		}
		
		protected override Beer GetNewEntity(bool includeId = true)
		{
			var id = includeId ? Guid.NewGuid() : Guid.Empty;
			return new Beer.Builder()
				.WithId(id)
				.Build();
		}
	}
}
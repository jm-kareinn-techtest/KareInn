using System;
using System.Collections.Generic;
using System.Linq;
using BeerDirectory.Core.Domain.Entities;
using BeerDirectory.Core.Repositories.Client;
using BeerDirectory.Core.Repositories.Exceptions;
using BeerDirectory.Core.Repositories.Filters;
using BeerDirectory.Core.Repositories.Interfaces;
using BeerDirectory.Core.Repositories.Interfaces.Filters;
using MongoDB.Driver;

namespace BeerDirectory.Core.Repositories
{
	public abstract class EntityRepository<T, TFilter> : IEntityRepository<T, TFilter>
		where T : Entity
		where TFilter : EntityFilter<T>, IEntityFilter<T>, new()
	{
		private readonly BeerDirectoryMongoClient _mongoClient;
		protected readonly IMongoCollection<T> Collection;
		protected FilterDefinition<T> DefaultReadFilter;
		protected List<T> Inserts { get; }
		
		public EntityRepository(BeerDirectoryMongoClient mongoClient)
		{
			_mongoClient = mongoClient;
			Collection = mongoClient.Set<T>();
			DefaultReadFilter = Builders<T>.Filter.Empty;
			Inserts = new List<T>();
		}

		public T GetById(Guid id)
		{
			var filter = DefaultReadFilter
			             & Builders<T>.Filter.Eq(x => x.Id, id);
			var query = Collection.Find(filter);
			var record = query.FirstOrDefault();
			if (record == default(T))
				throw new EntityNotFoundException();
			return record;
		}
		
		public IEnumerable<T> Get(TFilter filter)
		{
			var queryFilter = (DefaultReadFilter) & filter.GetFilterDefinition();
			var query = Collection.Find(queryFilter);
			query = query.Skip(filter.Skip)
				.Limit(filter.Take);

			var results = query.ToList();

			return results;
		}

		public void Add(T entity)
		{
			Inserts.Add(entity);
		}

		public void SaveChanges()
		{
			if (Inserts.Any())
				Collection.InsertMany(Inserts);
			Inserts.Clear();
		}

		public virtual long GetCount()
		{
			return Collection.CountDocuments(DefaultReadFilter);
		}

		public TFilter GetFilter()
		{
			return new TFilter();
		}
	}
}
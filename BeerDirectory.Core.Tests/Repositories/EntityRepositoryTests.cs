using System;
using System.Collections.Generic;
using System.Linq;
using BeerDirectory.Core.Domain.Entities;
using BeerDirectory.Core.Repositories;
using BeerDirectory.Core.Repositories.Client;
using BeerDirectory.Core.Repositories.Exceptions;
using BeerDirectory.Core.Repositories.Filters;
using BeerDirectory.Core.Repositories.Interfaces.Filters;
using Xunit;

namespace BeerDirectory.Core.Tests.Repositories
{
	public abstract class EntityRepositoryTests<T, TFilter>
		where T : Entity
		where TFilter : EntityFilter<T>, IEntityFilter<T>, new()
	{
		protected EntityRepository<T, TFilter> _repository;
		protected BeerDirectoryMongoClient _client;

		protected abstract T GetNewEntity(bool includeId = true);
		
		[Fact]
		public void GetById_returns_previously_added_entity()
		{
			// arrange
			var ent = AddEntity();

			// act
			var returnedEnt = _repository.GetById(ent.Id);

			// assert
			Assert.Equal(ent.Id, returnedEnt.Id);
		}
		
		[Fact]
		public void GetById_throws_if_entity_does_not_exist()
		{
			// act/assert
			Assert.Throws<EntityNotFoundException>(() => _repository.GetById(Guid.NewGuid()));
		}

		[Fact]
		public void Get_returns_any_added_entities()
		{
			// arrange
			var entities = AddManyEntities(5);
			
			// act
			var filter = _repository.GetFilter();
			filter.Skip = 0;
			filter.Take = 0;
			var returnedEntities = _repository.Get(filter);
			
			// assert
			var allEntitiesArePresent = entities.All(x => returnedEntities.Any(y => y.Id == x.Id));
			Assert.True(allEntitiesArePresent);
		}

		[Fact]
		public void Get_should_take_a_positive_number_of_results()
		{
			// arrange
			AddManyEntities(5);
			const int take = 3;
			
			// act
			var filter = _repository.GetFilter();
			filter.Skip = 0;
			filter.Take = take;
			var returnedEntities = _repository.Get(filter);
			
			// assert
			Assert.Equal(take, returnedEntities.Count());
		}

		protected T AddEntity()
		{
			var ent = GetNewEntity();
			_repository.Add(ent);
			_repository.SaveChanges();
			return ent;
		}

		protected IEnumerable<T> AddManyEntities(int count)
		{
			var entities = new List<T>();

			for (var i = 0; i < count; i++)
			{
				var ent = GetNewEntity();
				entities.Add(ent);
				_repository.Add(ent);
			}
			
			_repository.SaveChanges();

			return entities;
		}
	}
}
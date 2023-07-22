using System;
using System.Collections.Generic;
using System.Linq;
using BeerDirectory.Core.Domain.Entities;
using BeerDirectory.Core.Services.Exceptions;
using BeerDirectory.Core.Services.Filters;
using BeerDirectory.Core.Services.Interfaces;
using BeerDirectory.Core.Tests.Services.TestImplementations;
using Xunit;

namespace BeerDirectory.Core.Tests.Services
{
	public abstract class ApplicationModelServiceTests<T, TEntity, TFilter>
		where T : IApplicationServiceModel
		where TEntity : Entity
		where TFilter : ApplicationModelFilter, new()
	{
		private IApplicationModelService<T, TFilter> _service;
		private FakeEntityRepository<TEntity> _repository;
		private List<TEntity> _entities;
		private List<DateTime> _saveTimestamps;

		protected void InitialiseBaseTests(IApplicationModelService<T, TFilter> service,
			FakeEntityRepository<TEntity> repository, List<TEntity> entities, List<DateTime> saveTimestamps)
		{
			_service = service;
			_repository = repository;
			_entities = entities;
			_saveTimestamps = saveTimestamps;
		}
		
		public abstract TEntity GetNewEntity(Guid id);
		
		[Fact]
		public void Get_passes_ApplicationModelFilter_properties_to_repository_filter()
		{
			// arrange
			var ent = GetNewEntity(Guid.NewGuid());
			_entities.Add(ent);
			var filter = new TFilter
			{
				Take = 10,
				Skip = 10
			};
			
			// act
			_service.Get(filter);
			
			// assert
			var repoFilter = _repository.LastFilterUsed;
			var propsAreMapped =
				repoFilter.Skip == filter.Skip &&
				repoFilter.Take == filter.Take;
			Assert.True(propsAreMapped);
		}

		[Fact]
		public void Get_returns_List_of_T()
		{
			// arrange
			AddEntity();

			// act
			var entities = _service.Get(new TFilter());
			
			// assert
			Assert.IsType<List<T>>(entities);
		}

		[Fact]
		public void Get_must_have_skip_of_at_least_zero()
		{
			// arrange
			AddEntity();
			
			// assert/act
			var filter = new TFilter
			{
				Skip = -1
			};
			Assert.Throws<ArgumentOutOfRangeException>(() => _service.Get(filter));
		}

		[Fact]
		public void Get_must_have_a_take_of_at_least_zero()
		{
			// arrange
			AddEntity();
			
			// assert/act
			var filter = new TFilter
			{
				Take = -1
			};
			Assert.Throws<ArgumentOutOfRangeException>(() => _service.Get(filter));
		}

		[Fact]
		public void Get_with_a_take_of_zero_returns_all_results()
		{
			// arrange
			const int count = 10;
			AddManyEntities(count);
			
			// act
			var ents = _service.Get(new TFilter());
			
			// assert
			Assert.Equal(_entities.Count, ents.Count());
		}

		[Fact]
		public void GetById_returns_correct_entity()
		{
			// arrange
			const int count = 10;
			AddManyEntities(count);
			var selectedEnt = _entities.ElementAt(5);

			// act
			var ent = _service.GetById(selectedEnt.Id);
			
			// assert
			Assert.NotNull(ent);
		}

		[Fact]
		public void GetById_throws_ApplicationModelNotFound_if_entity_with_id_does_not_exist()
		{
			// act/assert
			Assert.Throws<ApplicationModelNotFoundException>(() => _service.GetById(Guid.NewGuid()));
		}

		private void AddEntity()
		{
			var ent = GetNewEntity(Guid.NewGuid());
			_entities.Add(ent);
		}

		private void AddManyEntities(int count)
		{
			for (var i = 0; i < count; i++)
			{
				AddEntity();
			}
		}
	}
}
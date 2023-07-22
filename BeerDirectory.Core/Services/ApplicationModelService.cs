using System;
using System.Collections.Generic;
using System.Linq;
using BeerDirectory.Core.Domain.Entities;
using BeerDirectory.Core.Repositories.Exceptions;
using BeerDirectory.Core.Repositories.Interfaces;
using BeerDirectory.Core.Repositories.Interfaces.Filters;
using BeerDirectory.Core.Services.Exceptions;
using BeerDirectory.Core.Services.Filters;
using BeerDirectory.Core.Services.Interfaces;

namespace BeerDirectory.Core.Services
{
	public abstract class ApplicationModelService<T, TEntity, TFilter, TEntityFilter> : IApplicationModelService<T, TFilter>
		where T : IApplicationServiceModel
		where TEntity : Entity
		where TFilter : ApplicationModelFilter
		where TEntityFilter : IEntityFilter<TEntity>
	{
		private readonly IEntityRepository<TEntity, TEntityFilter> _repository;

		public ApplicationModelService(IEntityRepository<TEntity, TEntityFilter> repository)
		{
			_repository = repository;
		}

		public abstract T ConvertEntityToModel(TEntity entity);
		
		public T GetById(Guid id)
		{
			var entity = GetDomainObject(id);
			var model = ConvertEntityToModel(entity);
			return model;
		}

		public List<T> Get(TFilter filter)
		{
			if (filter.Skip < 0 || filter.Take < 0) 
				throw new ArgumentOutOfRangeException();
			var repoFilter = _repository.GetFilter();
			MapFilter(filter, repoFilter);
			var results = _repository.Get(repoFilter);
			var models = results.Select(ConvertEntityToModel).ToList();

			return models;
		}

		protected TEntity GetDomainObject(Guid id)
		{
			try
			{
				return _repository.GetById(id);
			}
			catch (EntityNotFoundException e)
			{
				throw new ApplicationModelNotFoundException(e);
			}
		}
		
		protected virtual void MapFilter(TFilter applicationModelFilter, TEntityFilter entityFilter)
		{
			entityFilter.Skip = applicationModelFilter.Skip;
			entityFilter.Take = applicationModelFilter.Take;
		}
	}
}
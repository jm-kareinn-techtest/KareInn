using System;
using System.Collections.Generic;
using BeerDirectory.Core.Domain.Entities;
using BeerDirectory.Core.Repositories.Interfaces.Filters;

namespace BeerDirectory.Core.Repositories.Interfaces
{
	public interface IEntityRepository<T, TFilter>
		where T : Entity
		where TFilter : IEntityFilter<T>
	{
			T GetById(Guid id);
			IEnumerable<T> Get(TFilter filter);
			void Add(T entity);
			void SaveChanges();
			TFilter GetFilter();
			long GetCount();
			
	}
}
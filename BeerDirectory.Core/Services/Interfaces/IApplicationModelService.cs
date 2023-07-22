using System;
using System.Collections.Generic;
using BeerDirectory.Core.Services.Filters;

namespace BeerDirectory.Core.Services.Interfaces
{
	public interface IApplicationModelService<T, TFilter>
		where T : IApplicationServiceModel
		where TFilter : ApplicationModelFilter
	{
		T GetById(Guid id);
		List<T> Get(TFilter filter);
	}
}
using BeerDirectory.Core.Services.Filters;
using BeerDirectory.Core.Services.Models;

namespace BeerDirectory.Core.Services.Interfaces
{
	public interface IBeerService : IApplicationModelService<BeerModel, BeerModelFilter>
	{
		BeerModel Add(BeerModel model);
	}
}
namespace BeerDirectory.Core.Repositories.Interfaces.Filters
{
	public interface IEntityFilter<T>
	{
		int Skip { get; set; }
		int Take { get; set; }
	}
}
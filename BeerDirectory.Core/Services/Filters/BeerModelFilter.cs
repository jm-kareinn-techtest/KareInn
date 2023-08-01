namespace BeerDirectory.Core.Services.Filters
{
	public class BeerModelFilter : ApplicationModelFilter
	{
		public string? SearchTerms { get; set; }
		public int? Style { get; set; }
	}
}
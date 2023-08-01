using BeerDirectory.Core.Domain.Entities;
using BeerDirectory.Core.Repositories.Interfaces.Filters;
using MongoDB.Driver;

namespace BeerDirectory.Core.Repositories.Filters
{
    public class BeerFilter : EntityFilter<Beer>, IBeerFilter
    {
        public int? Style { get; set; }
        public string? SearchTerms { get; set; }

        public override FilterDefinition<Beer> GetFilterDefinition()
        {
            var filters = ConstructFilters();

            return filters.Count switch
            {
                > 1 => Builders<Beer>.Filter.And(filters),
                1 => filters.First(),
                _ => Builders<Beer>.Filter.Empty
            };
        }

        private List<FilterDefinition<Beer>> ConstructFilters()
        {
            var filters = new List<FilterDefinition<Beer>>();

            if (Style is >= 0)
            {
                filters.Add(Builders<Beer>.Filter.Eq(x => x.Style.Value, Style.Value));
            }

            if (SearchTerms is not null)
            {
                // The provided DB is not configured with a text index, so the following errors on use.
                //filters.Add(Builders<Beer>.Filter.Text(SearchTerms));
            }

            return filters;
        }
    }
}
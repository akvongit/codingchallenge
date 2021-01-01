using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ConstructionLine.CodingChallenge
{
    public class SearchHelper
    {
        public static Expression<Func<Shirt, bool>> BuildSearchPredicate(SearchOptions options)
        {
            PreprocessFilters(options);
            var queryPredicate = PredicateBuilder.New<Shirt>(true);
            queryPredicate = queryPredicate.And(x => !options.Colors.Any() || options.Colors.Contains(x.Color));
            queryPredicate = queryPredicate.And(x => !options.Sizes.Any() || options.Sizes.Contains(x.Size));
            return queryPredicate;
        }

        public static void UpdateCounts(SearchResults searchResult, SearchOptions options)
        {
            searchResult.ColorCounts = new List<ColorCount>();
            searchResult.SizeCounts = new List<SizeCount>();

            Color.All.ForEach(r =>
            {
                var colorCount = new ColorCount() { Color = r, Count = searchResult.Shirts.Where(x => x.Color == r).Count() };
                searchResult.ColorCounts.Add(colorCount);

            });

            Size.All.ForEach(r =>
            {
                var sizeCount = new SizeCount() { Size = r, Count = searchResult.Shirts.Where(x => x.Size == r).Count() };
                searchResult.SizeCounts.Add(sizeCount);
            });
        }

        private static void PreprocessFilters(SearchOptions options)
        {
            options.Colors = options.Colors ?? new List<Color>();
            options.Sizes = options.Sizes ?? new List<Size>();
        }
    }
}

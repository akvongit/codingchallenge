using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;
        
        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;
            // We use dynamic predicate building to support fetching on multiple options.
            // Runing a single shot query gives a better performance for 50K data set under question.
            // For Large Dataset We would need data partitioning and look up on small partition.
        }

        public SearchResults Search(SearchOptions options)
        {
            SearchResults result = new SearchResults();
            // TODO: search logic goes here.
            var queryPredicate = SearchHelper.BuildSearchPredicate(options);
            result.Shirts = _shirts.Where(queryPredicate.Compile()).ToList();
            SearchHelper.UpdateCounts(result, options);
            return result;
        }
    }
} 
﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using ConstructionLine.CodingChallenge.Tests.SampleData;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEnginePerformanceTests : SearchEngineTestsBase
    {
        private List<Shirt> _shirts;
        private SearchEngine _searchEngine;

        [SetUp]
        public void Setup()
        {
            
            var dataBuilder = new SampleDataBuilder(50000);

            _shirts = dataBuilder.CreateShirts();

            _searchEngine = new SearchEngine(_shirts);
        }


        [Test]
        public void PerformanceTest()
        {
            var sw = new Stopwatch();
            sw.Start();

            var options = new SearchOptions
            {
                Colors = new List<Color> { Color.Red, Color.Blue },
                Sizes = new List<Size> { Size.Small, Size.Medium }
            };

            var results = _searchEngine.Search(options);

            sw.Stop();
            Console.WriteLine($"This Test fixture finished in {sw.ElapsedMilliseconds} milliseconds");
            //Counts are asserted based on the search results not the original collections.
            AssertResults(results.Shirts, options);
            AssertSizeCounts(results.Shirts, options, results.SizeCounts);
            AssertColorCounts(results.Shirts, options, results.ColorCounts);
        }
    }
}

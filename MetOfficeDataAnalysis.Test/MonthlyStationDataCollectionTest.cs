using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using MetOfficeDataAnalysis.Lib;

namespace MetOfficeDataAnalysis.Test
{
    [TestFixture]
    class MonthlyStationDataCollectionTest : AssertionHelper
    {
        private MonthlyStationDataCollection sampleMonthlyData;

        [SetUp]
        public void SetUp()
        {
            sampleMonthlyData = new MonthlyStationDataCollection();

            sampleMonthlyData.Add(new MonthlyStationData(1948, 1, 8.9, 3.3, null, 85.0, null, null, false));
            sampleMonthlyData.Add(new MonthlyStationData(1948, 2, 7.9, 2.2, null, 26.0, null, null, false));
            sampleMonthlyData.Add(new MonthlyStationData(2005, 7, 23.3, 14.1, 0, 45.8, 202.5, true, false));
            sampleMonthlyData.Add(new MonthlyStationData(2005, 8, 23.2, 13.0, 0, 42.4, 250.4, true, false));
            sampleMonthlyData.Add(new MonthlyStationData(2012, 12, 9.0, 2.6, 10, 95.8, 58.0, false, false));
            sampleMonthlyData.Add(new MonthlyStationData(2013, 5, 16.4, 7.7, 0, 41.8, 163.3, false, true));
        }

        [TearDown]
        public void TearDown()
        {
            sampleMonthlyData = null;
        }

        [Test]
        public void FindHottestMaxTemperature()
        {
            var expected = new MonthlyStationData(2005, 7, 23.3, 14.1, 0, 45.8, 202.5, true, false);
            var actual = sampleMonthlyData.HottestMonth;

            Expect(actual, Is.EqualTo(expected));
        }

        [Test]
        public void FindHottestMaxTemperatureAvoidNulls()
        {
            sampleMonthlyData.Add(new MonthlyStationData(2005, 7, null, 14.1, 0, 45.8, 202.5, true, false));

            var expected = new MonthlyStationData(2005, 7, 23.3, 14.1, 0, 45.8, 202.5, true, false);
            var actual = sampleMonthlyData.HottestMonth;

            Expect(actual, Is.EqualTo(expected));
        }

        [Test]
        public void FindMonthWithColdestMaxTemperature()
        {
            var expected = new MonthlyStationData(1948, 2, 7.9, 2.2, null, 26.0, null, null, false);
            var actual = sampleMonthlyData.MonthWithColdestMaxTemperature;

            Expect(actual, Is.EqualTo(expected));
        }

        [Test]
        public void FindMonthWithColdestMaxTemperatureAvoidNulls()
        {
            sampleMonthlyData.Add(new MonthlyStationData(1947, 1, null, 3.3, null, 85.0, null, null, false));

            var expected = new MonthlyStationData(1948, 2, 7.9, 2.2, null, 26.0, null, null, false);
            var actual = sampleMonthlyData.MonthWithColdestMaxTemperature;

            Expect(actual, Is.EqualTo(expected));
        }

        [Test]
        public void FindColdestMinTemperature()
        {
            var expected = new MonthlyStationData(1948, 2, 7.9, 2.2, null, 26.0, null, null, false);
            var actual = sampleMonthlyData.ColdestMonth;

            Expect(actual, Is.EqualTo(expected));
        }

        [Test]
        public void FindColdestMinTemperatureAvoidNulls()
        {
            sampleMonthlyData.Add(new MonthlyStationData(1947, 2, 7.9, null, null, 26.0, null, null, false));

            var expected = new MonthlyStationData(1948, 2, 7.9, 2.2, null, 26.0, null, null, false);
            var actual = sampleMonthlyData.ColdestMonth;

            Expect(actual, Is.EqualTo(expected));
        }

        [Test]
        public void FindMonthWithHottestMinTemperature()
        {
            var expected = new MonthlyStationData(2005, 7, 23.3, 14.1, 0, 45.8, 202.5, true, false);
            var actual = sampleMonthlyData.MonthWithHottestMinTemperature;

            Expect(actual, Is.EqualTo(expected));
        }

        [Test]
        public void FindMonthWithHottestMinTemperatureAvoidNulls()
        {
            sampleMonthlyData.Add(new MonthlyStationData(1947, 7, 23.3, null, 0, 45.8, 202.5, true, false));

            var expected = new MonthlyStationData(2005, 7, 23.3, 14.1, 0, 45.8, 202.5, true, false);
            var actual = sampleMonthlyData.MonthWithHottestMinTemperature;

            Expect(actual, Is.EqualTo(expected));
        }

        [Test]
        public void FindMonthlyMeanMaxTemperatures()
        {
            sampleMonthlyData.Add(new MonthlyStationData(2004, 7, 22.7, 13.3, 0, 37.6, 201.5, true, false));

            var expected = new SortedDictionary<int, double>();
            expected.Add(1, 8.9);
            expected.Add(2, 7.9);
            expected.Add(5, 16.4);
            expected.Add(7, 23.0);
            expected.Add(8, 23.2);
            expected.Add(12, 9.0);

            var actual = sampleMonthlyData.MeanMaxTemperatures;

            Expect(actual, Is.EqualTo(expected));
        }
    }
}

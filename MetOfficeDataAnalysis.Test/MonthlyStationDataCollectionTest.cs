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
            var expectedHottestMonth = new MonthlyStationData(2005, 7, 23.3, 14.1, 0, 45.8, 202.5, true, false);

            var actualHottestMonth = sampleMonthlyData.HottestMonth;

            Expect(actualHottestMonth, Is.EqualTo(expectedHottestMonth));
        }
    }
}

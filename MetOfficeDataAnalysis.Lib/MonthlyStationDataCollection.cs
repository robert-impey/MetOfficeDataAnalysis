using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MetOfficeDataAnalysis.Lib
{
    public class MonthlyStationDataCollection : Collection<MonthlyStationData>
    {
        public MonthlyStationData HottestMonth
        {
            get
            {
                return this.Where(n => n.MaxTemperature != null).OrderByDescending(n => n.MaxTemperature).First();
            }
        }

        public MonthlyStationData MonthWithColdestMaxTemperature
        {
            get
            {
                return this
                    .Where(n => n.MaxTemperature != null)
                    .OrderBy(n => n.MaxTemperature)
                    .First();
            }
        }

        public MonthlyStationData ColdestMonth
        {
            get
            {
                return this
                    .Where(n => n.MinTemperature != null)
                    .OrderBy(n => n.MinTemperature)
                    .First();
            }
        }

        public MonthlyStationData MonthWithHottestMinTemperature
        {
            get
            {
                return this.Where(n => n.MinTemperature != null).OrderByDescending(n => n.MinTemperature).First();
            }
        }

        public SortedDictionary<int, double> MeanMaxTemperatures
        {
            get
            {
                var meanMaxTemperature = new SortedDictionary<int, double>();

                var groups = from data in this
                             where data.MaxTemperature != null
                             group data by data.Month;

                foreach (var group in groups)
                {
                    meanMaxTemperature[group.Key] = group.Average(d => d.MaxTemperature.Value);
                }

                return meanMaxTemperature;
            }
        }
    }
}

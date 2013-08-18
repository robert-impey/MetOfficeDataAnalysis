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
                return this.OrderByDescending(n => n.MaxTemperature).First();
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
                return this.OrderByDescending(n => n.MinTemperature).First();
            }
        }

        public SortedDictionary<int, double> MeanMaxTemperatures
        {
            get
            {
                var sumMaxTemperatures = new Dictionary<int, double>();
                var countMaxTemperatures = new Dictionary<int, int>();

                foreach (var data in this)
                {
                    if (data.MaxTemperature != null)
                    {
                        if (countMaxTemperatures.ContainsKey(data.Month))
                        {
                            countMaxTemperatures[data.Month]++;
                        }
                        else
                        {
                            countMaxTemperatures[data.Month] = 1;
                        }

                        double currentSumOfMaxTemperatures;
                        if (sumMaxTemperatures.TryGetValue(data.Month, out currentSumOfMaxTemperatures))
                        {
                            sumMaxTemperatures[data.Month] = currentSumOfMaxTemperatures + data.MaxTemperature.Value;
                        }
                        else
                        {
                            sumMaxTemperatures[data.Month] = data.MaxTemperature.Value;
                        }
                    }
                }

                var meanMaxTemperature = new SortedDictionary<int, double>();

                foreach (var month in sumMaxTemperatures.Keys)
                {
                    meanMaxTemperature[month] = sumMaxTemperatures[month] / countMaxTemperatures[month];
                }

                return meanMaxTemperature;
            }
        }
    }
}

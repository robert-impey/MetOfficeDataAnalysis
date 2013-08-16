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
    }
}

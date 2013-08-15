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
    }
}

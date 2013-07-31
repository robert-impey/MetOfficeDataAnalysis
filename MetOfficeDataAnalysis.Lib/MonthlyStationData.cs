using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetOfficeDataAnalysis.Lib
{
    public class MonthlyStationData
    {
        public int Year { get; private set; }
        public int Month { get; set; } // 1 to 12
        public double? MaxTemperature { get; private set; } // Deg. C
        public double? MinTemperature { get; private set; }
        public int? AirFrost { get; private set; } // Days
        public double? Rain { get; private set; } // mm
        public double? Sunshine { get; private set; } // Hours
        public bool? CampbellStokes { get; private set; } // Sunshine hours measured with Kipp & Zonen or Campbell Stokes device.
        public bool Provisional { get; private set; }

        public MonthlyStationData(int year, int month, double? maxTemperature, double? minTemperature, 
            int? airFrost, double? rain, double? sunshine, bool? campbellStokes, bool provisional)
        {
            Year = year;
            Month = month;
            MaxTemperature = maxTemperature;
            MinTemperature = minTemperature;
            AirFrost = airFrost;
            Rain = rain;
            Sunshine = sunshine;
            CampbellStokes = campbellStokes;
            Provisional = provisional;
        }
    }
}

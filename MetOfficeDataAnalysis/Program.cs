using MetOfficeDataAnalysis.Lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MetOfficeDataAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                var stationDataFile = new StationDataFile(new StreamReader(args[0]));

                Console.WriteLine(String.Format("Station name: {0}", stationDataFile.StationName));

                var hottestMonth = stationDataFile.MonthlyData.HottestMonth;
                PrintMonthTemperature("Hottest month", hottestMonth.Year, hottestMonth.Month, hottestMonth.MaxTemperature);
                var monthWithColdestMaxTemperature = stationDataFile.MonthlyData.MonthWithColdestMaxTemperature;
                PrintMonthTemperature("Month with coldest max temperature",
                    monthWithColdestMaxTemperature.Year, monthWithColdestMaxTemperature.Month,
                    monthWithColdestMaxTemperature.MaxTemperature);

                var coldestMonth = stationDataFile.MonthlyData.ColdestMonth;
                PrintMonthTemperature("Coldest month", coldestMonth.Year, coldestMonth.Month, coldestMonth.MinTemperature);
                var monthWithHottestMinTemperature = stationDataFile.MonthlyData.MonthWithHottestMinTemperature;
                PrintMonthTemperature("Month with hottest min temperature",
                    monthWithHottestMinTemperature.Year, monthWithHottestMinTemperature.Month,
                    monthWithHottestMinTemperature.MinTemperature);
            }
            else
            {
                Console.WriteLine("Not sure what to do!");
            }
        }

        private static void PrintMonthTemperature(string description, int year, int month, double? temperature)
        {
            Console.WriteLine(String.Format("{0}: {1}-{2}, Temp: {3} C",
                description, year, month, temperature));
        }
    }
}

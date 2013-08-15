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
                Console.WriteLine(String.Format("Hottest Month: {0}-{1}, Temp: {2} C", 
                    hottestMonth.Year, hottestMonth.Month, hottestMonth.MaxTemperature));
            }
            else
            {
                Console.WriteLine("Not sure what to do!");
            }
        }
    }
}

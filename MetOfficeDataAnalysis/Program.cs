using MetOfficeDataAnalysis.Lib;
using System;
using System.IO;

namespace MetOfficeDataAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                var path = args[0];
                if (File.Exists(path))
                {
                    PrintStationFileData(path);
                }
                else if (Directory.Exists(path))
                {
                    string[] fileEntries = Directory.GetFiles(path);
                    foreach (string fileName in fileEntries)
                        PrintStationFileData(fileName);
                }
                else
                {
                    Console.WriteLine("{0} is not a valid file or directory.", path);
                }
            }
            else
            {
                Console.WriteLine("Not sure what to do!");
            }
        }

        private static void PrintStationFileData(string fileName)
        {
            var stationDataFile = new StationDataFile(new StreamReader(fileName));

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

            Console.WriteLine("Mean monthly max temperatures");
            var meanMonthlyMaxTemperatures = stationDataFile.MonthlyData.MeanMaxTemperatures;
            foreach (var month in meanMonthlyMaxTemperatures.Keys)
            {
                Console.WriteLine(String.Format("{0} - {1:#.00} C", month, meanMonthlyMaxTemperatures[month]));
            }
        }

        private static void PrintMonthTemperature(string description, int year, int month, double? temperature)
        {
            Console.WriteLine(String.Format("{0}: {1}-{2}, Temp: {3} C",
                description, year, month, temperature));
        }
    }
}

using MetOfficeDataAnalysis.Lib;

namespace MetOfficeDataAnalysis;

internal class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length == 1)
        {
            var path = args[0];
            if (File.Exists(path))
            {
                await PrintStationFileData(path);
            }
            else if (Directory.Exists(path))
            {
                string[] fileEntries = Directory.GetFiles(path);
                foreach (string fileName in fileEntries)
                    await PrintStationFileData(fileName);
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
    private static async Task PrintStationFileData(string fileName)
    {
        var stationDataFile = await StationDataFile.LoadAsync(new StreamReader(fileName));

        Console.WriteLine($"Station name: {stationDataFile.StationName}");

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
        foreach (var kvp in meanMonthlyMaxTemperatures)
        {
            Console.WriteLine($"{kvp.Key} - {kvp.Value:#.00} C");
        }
    }

    private static void PrintMonthTemperature(string description, int year, int month, double? temperature)
    {
        Console.WriteLine($"{description}: {year}-{month}, Temp: {temperature} C");
    }
}
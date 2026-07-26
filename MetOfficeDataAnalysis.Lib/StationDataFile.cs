namespace MetOfficeDataAnalysis.Lib;

public class StationDataFile
{
    public string StationName { get; private set; }
    public MonthlyStationDataCollection MonthlyData { get; private set; }
    // Make the ctor private so instances are created via the loader methods
    private StationDataFile()
    {
    }

    // Async loader - reads from a TextReader using async APIs and returns a fully constructed instance
    public static async Task<StationDataFile> LoadAsync(TextReader reader)
    {
        if (reader == null) throw new ArgumentNullException(nameof(reader));

        var instance = new StationDataFile();

        var line = await reader.ReadLineAsync().ConfigureAwait(false);
        if (line == null) throw new Exception("Empty station file");
        instance.StationName = line.Trim();

        instance.MonthlyData = new MonthlyStationDataCollection();

        while (true)
        {
            line = await reader.ReadLineAsync().ConfigureAwait(false);
            if (line == "              degC    degC    days      mm   hours")
            {
                break;
            }

            if (line == null)
            {
                throw new Exception("End of file reached before getting to the data!");
            }
        }

        while (true)
        {
            line = await reader.ReadLineAsync().ConfigureAwait(false);

            if (line == null)
            {
                break;
            }

            MonthlyStationData msd = null;
            if (ParseDataLine(line, ref msd))
            {
                instance.MonthlyData.Add(msd);
            }
        }

        return instance;
    }

    // Synchronous loader kept for convenience
    public static StationDataFile Load(TextReader reader)
    {
        if (reader == null) throw new ArgumentNullException(nameof(reader));

        var instance = new StationDataFile();

        var line = reader.ReadLine();
        if (line == null) throw new Exception("Empty station file");
        instance.StationName = line.Trim();

        instance.MonthlyData = new MonthlyStationDataCollection();

        while (true)
        {
            line = reader.ReadLine();
            if (line == "              degC    degC    days      mm   hours")
            {
                break;
            }

            if (line == null)
            {
                throw new Exception("End of file reached before getting to the data!");
            }
        }

        while (true)
        {
            line = reader.ReadLine();

            if (line == null)
            {
                break;
            }

            MonthlyStationData msd = null;
            if (ParseDataLine(line, ref msd))
            {
                instance.MonthlyData.Add(msd);
            }
        }

        return instance;
    }

    public static bool ParseDataLine(string line, ref MonthlyStationData monthlyStationData)
    {
        if (String.IsNullOrWhiteSpace(line))
        {
            return false;
        }

        char[] delimiters = { ' ' };

        var parts = line.Trim().Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

        var i = 0;
        var yearPart = parts.Length > i ? parts[i++] : "";
        var monthPart = parts.Length > i ? parts[i++] : "";
        var maxTemperaturePart = parts.Length > i ? parts[i++] : "";
        var minTemperaturePart = parts.Length > i ? parts[i++] : "";
        var airFrostPart = parts.Length > i ? parts[i++] : "";
        var rainPart = parts.Length > i ? parts[i++] : "";
        var sunshinePart = parts.Length > i ? parts[i++] : "";
        var provisionalPart = parts.Length > i ? parts[i++] : "";

        if (int.TryParse(yearPart, out var year) && int.TryParse(monthPart, out var month))
        {
            double? maxTemperature = PartToNullableDouble(maxTemperaturePart);
            double? minTemperature = PartToNullableDouble(minTemperaturePart);

            int? airFrost = null;
            if (!DatumIsMissing(airFrostPart))
            {
                if (int.TryParse(airFrostPart, out var airFrostValue))
                {
                    airFrost = airFrostValue;
                }
            }

            double? rain = PartToNullableDouble(rainPart);

            double? sunshine = null;
            bool? campbellStokes = null;
            if (!DatumIsMissing(sunshinePart))
            {
                if (sunshinePart.EndsWith('#'))
                {
                    campbellStokes = false;
                    var idx = sunshinePart.IndexOf('#');
                    if (idx >= 0)
                        sunshinePart = sunshinePart.Remove(idx);
                }
                else
                {
                    campbellStokes = true;
                }

                sunshine = PartToNullableDouble(sunshinePart);
            }

            var provisional = provisionalPart == "Provisional";

            monthlyStationData = new MonthlyStationData(year, month, maxTemperature, minTemperature,
                airFrost, rain, sunshine, campbellStokes, provisional);

            return true;
        }

        return false;
    }

    private static bool DatumIsMissing(string datum) => datum == "---";

    private static double? PartToNullableDouble(string part)
    {
        if (DatumIsMissing(part))
            return null;

        return double.TryParse(part, out var value) ? (double?)value : null;
    }
}
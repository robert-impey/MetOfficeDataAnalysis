using System;
using System.IO;

namespace MetOfficeDataAnalysis.Lib
{
    public class StationDataFile
    {
        public string StationName { get; private set; }
        public MonthlyStationDataCollection MonthlyData { get; private set; }

        public StationDataFile(TextReader reader)
        {
            StationName = reader.ReadLine().Trim();

            MonthlyData = new MonthlyStationDataCollection();

            while (true)
            {
                var line = reader.ReadLine();
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
                var line = reader.ReadLine();

                if (line == null)
                {
                    break;
                }

                MonthlyStationData msd = null;
                if (ParseDataLine(line, ref msd))
                {
                    MonthlyData.Add(msd);
                }
            }
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

            int year, month;
            if (int.TryParse(yearPart, out year) && int.TryParse(monthPart, out month))
            {
                double? maxTemperature = PartToNullableDouble(maxTemperaturePart);
                double? minTemperature = PartToNullableDouble(minTemperaturePart);

                int? airFrost = null;
                if (!DatumIsMissing(airFrostPart))
                {
                    int airFrostValue;
                    if (int.TryParse(airFrostPart, out airFrostValue))
                    {
                        airFrost = airFrostValue;
                    }
                }

                double? rain = PartToNullableDouble(rainPart);

                double? sunshine = null;
                bool? campbellStokes = null;
                if (!DatumIsMissing(sunshinePart))
                {
                    if (sunshinePart.EndsWith("#"))
                    {
                        campbellStokes = false;
                        sunshinePart = sunshinePart.Remove(sunshinePart.IndexOf('#'));
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

        private static bool DatumIsMissing(string datum)
        {
            return datum == "---";
        }

        private static double? PartToNullableDouble(string part)
        {
            double? datum = null;
            if (!DatumIsMissing(part))
            {
                double value;
                if (double.TryParse(part, out value))
                {
                    datum = value;
                }
            }

            return datum;
        }
    }
}

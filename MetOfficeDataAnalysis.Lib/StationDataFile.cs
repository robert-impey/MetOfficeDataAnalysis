using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

                MonthlyData.Add(ParseDataLine(line));
            }
        }

        public static MonthlyStationData ParseDataLine(string line)
        {
            if (String.IsNullOrWhiteSpace(line))
            {
                throw new ArgumentException();
            }

            char[] delimiters = { ' ' };

            var parts = line.Trim().Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            var yearPart = parts[0];
            var monthPart = parts[1];
            var maxTemperaturePart = parts[2];
            var minTemperaturePart = parts[3];
            var airFrostPart = parts[4];
            var rainPart = parts[5];
            var sunshinePart = parts[6];

            var provisionalPart = "";
            if (parts.Length > 7)
            {
                provisionalPart = parts[7];
            }

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

                return new MonthlyStationData(year, month, maxTemperature, minTemperature,
                    airFrost, rain, sunshine, campbellStokes, provisional);
            }

            throw new ArgumentException("Unable to parse line!");
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

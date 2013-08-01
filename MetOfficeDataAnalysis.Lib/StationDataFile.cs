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
        public ICollection<MonthlyStationData> MonthlyData { get; private set; }

        public StationDataFile(TextReader reader)
        {
            StationName = reader.ReadLine();

            MonthlyData = new LinkedList<MonthlyStationData>();

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

                MonthlyData.Add(ParseDataLine(line));

                if (line == null)
                {
                    break;
                }
            }
        }

        public static MonthlyStationData ParseDataLine(string line)
        {
            throw new ArgumentException("Unable to parse line!");
        }
    }
}

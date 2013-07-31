using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MetOfficeDataAnalysis.Lib
{
    public class StationDataFile
    {
        private readonly TextReader reader;
        public string StationName { get; private set; }

        public StationDataFile(TextReader aReader)
        {
            reader = aReader;

            StationName = reader.ReadLine();
        }
    }
}

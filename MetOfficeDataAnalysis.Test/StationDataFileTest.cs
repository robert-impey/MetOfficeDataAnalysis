using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;
using MetOfficeDataAnalysis.Lib;

namespace MetOfficeDataAnalysis.Test
{
    [TestFixture]
    class StationDataFileTest : AssertionHelper
    {
        // From http://www.metoffice.gov.uk/climate/uk/stationdata/
        private const string heathrowStationDataFileContent = @"Heathrow (London Airport)
Location 5078E 1767N 25m amsl
Estimated data is marked with a * after the value.
Missing data (more than 2 days missing in month) is marked by  ---.
Sunshine data taken from an automatic Kipp & Zonen sensor marked with a #, otherwise sunshine data taken from a Campbell Stokes recorder.
   yyyy  mm   tmax    tmin      af    rain     sun
              degC    degC    days      mm   hours
   1948   1    8.9     3.3    ---     85.0    ---
   1948   2    7.9     2.2    ---     26.0    ---
   1948   3   14.2     3.8    ---     14.0    ---
   1948   4   15.4     5.1    ---     35.0    ---
   1948   5   18.1     6.9    ---     57.0    ---
   1948   6   19.1    10.3    ---     67.0    ---
   1948   7   21.7    12.0    ---     21.0    ---
   1948   8   20.8    11.7    ---     67.0    ---
   1948   9   19.6    10.2    ---     35.0    ---
   1948  10   14.9     6.0    ---     50.0    ---
   1948  11   10.8     4.6    ---     44.0    ---
   1948  12    8.8     3.8    ---     63.0    ---
   1949   1    8.5     1.8       9    23.0    ---
   1949   2   10.4     0.6      11    27.0    ---
   1949   3    9.3     1.2      11    26.1    ---
   1949   4   16.2     6.0       1    34.2    ---
   1949   5   17.1     6.8       0    56.9    ---
   1949   6   22.0    10.5       0     9.0    ---
   1949   7   25.1    12.9       0    46.5    ---
   1949   8   23.9    12.5       0    26.3    ---
   1949   9   22.8    13.3       0    23.3    ---
   1949  10   17.0     8.6       3   139.6    ---
   1949  11   10.2     2.9       7    53.4    ---
   1949  12    9.2     2.9       6    33.0    ---";

        [Test]
        public void ExtractStationName()
        {
            var expectedStationName = "Heathrow (London Airport)";

            var stationDataFile = new StationDataFile(new StringReader(heathrowStationDataFileContent));

            var actualStationName = stationDataFile.StationName;

            Expect(actualStationName, Is.EqualTo(expectedStationName));
        }
    }
}

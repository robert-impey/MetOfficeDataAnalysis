/// Data for one month from one station.
namespace MetOfficeDataAnalysis.Lib;

public record MonthlyStationData(
    int Year,
    int Month,
    double? MaxTemperature,
    double? MinTemperature,
    int? AirFrost,
    double? Rain,
    double? Sunshine,
    bool? CampbellStokes,
    bool Provisional);

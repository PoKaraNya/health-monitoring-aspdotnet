namespace server.Services;

public interface IRoomRecordRequest
{
    double Humidity { get; }
    double Temperature { get; }
    double Pressure { get; }
    double CarbonDioxide { get; }
    double AirIons { get; }
    double Ozone { get; }
} 

public interface IRoomRecordService
{
    bool IsCriticalResults(IRoomRecordRequest createRoomRecordsDto);
}

public class RoomRecordService : IRoomRecordService
{
    public bool IsCriticalResults(IRoomRecordRequest createRoomRecordsDto)
    {
        var humidity = createRoomRecordsDto.Humidity;
        var temperature = createRoomRecordsDto.Temperature;
        var pressure = createRoomRecordsDto.Pressure;
        var carbonDioxide = createRoomRecordsDto.CarbonDioxide;
        var airIons = createRoomRecordsDto.AirIons;
        var ozone = createRoomRecordsDto.Ozone;

        var isHumidityValid = humidity >= 40 && humidity <= 60;
        var isTemperatureValid = temperature >= 19 && temperature <= 24;
        var isPressureValid = pressure >= 750 && pressure <= 770;
        var isCarbonDioxideValid = carbonDioxide >= 400 && carbonDioxide <= 600;
        var isAirIonsValid = airIons >= 400 && airIons <= 600;
        var isOzoneValid = ozone >= 0.1 && ozone <= 0.16;

        return (
            !isHumidityValid ||
            !isTemperatureValid ||
            !isPressureValid ||
            !isCarbonDioxideValid ||
            !isAirIonsValid ||
            !isOzoneValid
        );
    }
}

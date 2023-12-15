namespace server.Services;

public interface IPersonRecordRequest
{
    double Saturation { get; }
    double HeartRate { get; }
    double Temperature { get; }
} 

public interface IPersonRecordService
{
    bool IsCriticalResults(IPersonRecordRequest createPersonRecordsDto);
}

public class PersonRecordService : IPersonRecordService
{
    public bool IsCriticalResults(IPersonRecordRequest createPersonRecordsDto)
    {
        var saturation = createPersonRecordsDto.Saturation;
        var heartRate = createPersonRecordsDto.HeartRate;
        var temperature = createPersonRecordsDto.Temperature;

        var isSaturationValid = saturation >= 95 && saturation <= 100;
        var isHeartRateValid = heartRate >= 60 && heartRate <= 70;
        var isTemperatureValid = temperature >= 36.4 && temperature <= 36.8;

        return (
           !isSaturationValid ||
            !isHeartRateValid ||
            !isTemperatureValid
        );
    }
}

﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace server.Models.DTO.RoomRecord;

public class CreateRoomRecordByDeviceRequestDto
{
    [ForeignKey("Room")]
    public string RoomNumber { get; set; }
    [Required]
    public double Humidity { get; set; }
    [Required]
    public double Temperature { get; set; }
    [Required]
    public double Pressure { get; set; }
    [Required]
    public double CarbonDioxide { get; set; }
    [Required]
    public double AirIons { get; set; }
    [Required]
    public double Ozone { get; set; }
    public bool IsCriticalResults { get; set; }
    //public DateTime RecordedDate { get; set; }
}

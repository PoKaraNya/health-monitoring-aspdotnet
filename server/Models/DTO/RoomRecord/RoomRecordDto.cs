﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace server.Models.DTO.RoomRecord
{
    public class RoomRecordDto
    {
        public int RoomRecordId { get; set; }
        [ForeignKey("Room")]
        public int RoomId { get; set; }
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
        public DateTime RecordedDate { get; set; }
    }
}

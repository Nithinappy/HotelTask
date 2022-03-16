using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using HotelTask.Models;

namespace HotelTask.DTOs;

public record RoomDTO
{
    [JsonPropertyName("id")]
    public int RoomId { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("staff_name")]
    public string StaffName { get; set; }
    
    [JsonPropertyName("room_staffs")]
    public List<StaffDTO> Staffs { get; set; }
     [JsonPropertyName("schedules")]
    public List<ScheduleDTO> Schedules { get; set; }

  
}


public record CreateRoomDTO
{
    [Required]
    [MinLength(3)]
    [MaxLength(30)]


    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("size")]
    [Required]
    public int Size { get; set; }
    [JsonPropertyName("price")]
    [Required]
    public double Price { get; set; }

    [JsonPropertyName("staff_id")]
    [Required]
    public int StaffId { get; set; }
}


public record UpdateRoomDTO
{
    [Required]
    [MinLength(3)]
    [MaxLength(30)]


    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("size")]
    [Required]
    public int? Size { get; set; }
    [JsonPropertyName("price")]
    [Required]
    public decimal? Price { get; set; }


}
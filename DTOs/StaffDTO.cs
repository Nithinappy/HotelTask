using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using HotelTask.Models;

namespace HotelTask.DTOs;

public record StaffDTO
{
     [JsonPropertyName("staff_id")]
    public int StaffId { get; set; }
     [JsonPropertyName("name")]
    public string Name { get; set; }
     [JsonPropertyName("date_of_birth")]
    public DateTimeOffset DateOfBirth { get; set; }
     [JsonPropertyName("gender")]
    public string Gender { get; set; }
     [JsonPropertyName("mobile")]
    public long Mobile { get; set; }
     [JsonPropertyName("shift")]
    public string Shift { get; set; }
     [JsonPropertyName("rooms")]
    public List<RoomDTO> Rooms{ get; set; }
}

public record StaffCreateDTO
{
    [JsonPropertyName("name")]
    [Required]
    [MinLength(3)]
    [MaxLength(255)]
    public string Name { get; set; }

    [JsonPropertyName("mobile")]
    [Required]
    public long Mobile { get; set; }

    [JsonPropertyName("date_of_birth")]
    [Required]
    public DateTimeOffset DateOfBirth { get; set; }

    [JsonPropertyName("shift")]
    [MaxLength(255)]
    public string Shift { get; set; }

    [JsonPropertyName("gender")]
    [Required]
    public string Gender { get; set; }
}


public record StaffUpdateDTO
{
    [JsonPropertyName("name")]
    [Required]
    [MinLength(3)]
    [MaxLength(255)]
    public string Name { get; set; }
    [JsonPropertyName("date_of_birth")]
    [Required]
    public DateTimeOffset DateOfBirth { get; set; }
    [JsonPropertyName("gender")]
    [Required]
    public string Gender { get; set; }

    [JsonPropertyName("mobile")]
    [Required]
    public long Mobile { get; set; }
    [JsonPropertyName("shift")]
    [MaxLength(255)]
    public string Shift { get; set; }


}
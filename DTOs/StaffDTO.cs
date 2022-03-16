using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using HotelTask.Models;

namespace HotelTask.DTOs;

public record StaffDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public string Gender { get; set; }
    public long Mobile { get; set; }
    public string Shift { get; set; }
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

    [JsonPropertyName("email")]
    [Required]
    [MaxLength(255)]
    public string Email { get; set; }

    [JsonPropertyName("date_of_birth")]
    [Required]
    public DateTimeOffset DateOfBirth { get; set; }

    [JsonPropertyName("address")]
    [MaxLength(255)]
    public string Address { get; set; }

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

    [JsonPropertyName("mobile")]
    [Required]
    public long Mobile { get; set; }

    [JsonPropertyName("email")]
    [Required]
    [MaxLength(255)]
    public string Email { get; set; }

    [JsonPropertyName("date_of_birth")]
    [Required]
    public DateTimeOffset DateOfBirth { get; set; }

    [JsonPropertyName("address")]
    [MaxLength(255)]
    public string Address { get; set; }


}
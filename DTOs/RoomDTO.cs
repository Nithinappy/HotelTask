using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using HotelTask.Models;

namespace HotelTask.DTOs;

public record RoomDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("staff_name")]
    public string StaffName { get; set; }
}
// type smallint NOT NULL,
//    size integer NOT NULL,
//     price numeric(10,2) NOT NULL,
//     staff_id integer

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
    public int Size { get; set; }
    [JsonPropertyName("price")]
    [Required]
    public decimal Price { get; set; }

    [JsonPropertyName("staff_name")]
    [Required]
    public string StaffName { get; set; }
}
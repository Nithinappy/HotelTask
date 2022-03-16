using HotelTask.DTOs;
namespace HotelTask.Models;



public record Room
{
    public int RoomId { get; set; }
    public string Type { get; set; }
    public int Size { get; set; }
    public double Price { get; set; }
    public int StaffId { get; set; }
    public string StaffName { get; set; }

    public RoomDTO asDto => new RoomDTO
    {
        RoomId = RoomId,
        Size = Size,
        Type = Type,
        StaffName = StaffName,
    };
}
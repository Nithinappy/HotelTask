using Microsoft.AspNetCore.Mvc;
using HotelTask.Models;
using HotelTask.Repositories;
using HotelTask.DTOs;

namespace HotelTask.Controllers;

[ApiController]
[Route("api/room")]
public class RoomController : ControllerBase
{
    private readonly ILogger<RoomController> _logger;
    private readonly IRoomRepository _room;
    private readonly IStaffRepository _staff;
    private readonly IScheduleRepository _schedule;

    public RoomController(ILogger<RoomController> logger, IRoomRepository _room,IStaffRepository staff,IScheduleRepository schedule)
    {
        _logger = logger;
        this._room = _room;
        _staff =staff;
        _schedule = schedule;
    }

    [HttpGet]
    public async Task<ActionResult> GetList()
    {

        var res = await _room.GetList();

        return Ok(res.Select(x => x.asDto));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoomDTO>> GetById([FromRoute] int id)
    {
        var res = await _room.GetById(id);

        if (res is null)
            return NotFound();
        var dto = res.asDto;
       
        dto.Staffs = (await _staff.GetStaffRoomById(id)).Select(x => x.asDto).ToList();
        dto.Schedules = (await _schedule.GetScheduleByRoomId(id))
                        .Select(x => x.asDto).ToList();
        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateRoomDTO Data)
    {
        
        var toCreateRoom = new Room
        {
            Type = Data.Type,
            Size = Data.Size,
            Price = Data.Price,
            StaffId = Data.StaffId


        };

        var res = await _room.Create(toCreateRoom);

        return StatusCode(StatusCodes.Status201Created, res.asDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update([FromRoute] int id,
    [FromBody] UpdateRoomDTO Data)
    {
        var existing = await _room.GetById(id);
        if (existing is null)
            return NotFound("No Room found with given Room Id");

        var toUpdateRoom = existing with
        {

            Type=Data.Type ?? existing.Type,
            Size=Data.Size ?? existing.Size,
            Price=Data.Size ?? existing.Price,
           
        };

        var didUpdate = await _room.Update(toUpdateRoom);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update user");

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        var existing = await _room.GetById(id);
        if (existing is null)
            return NotFound("No Room found with given Room Id");

        var didDelete = await _room.Delete(id);

        return NoContent();
    }
}

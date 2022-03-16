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

    public RoomController(ILogger<RoomController> logger, IRoomRepository _room)
    {
        _logger = logger;
        this._room = _room;
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

        return Ok(res.asDto);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateRoomDTO Data)
    {
        var toCreateRoom = new Room
        {
            // Type = Data.Type,
            Size = Data.Size,
            Price = Data.Price,
            StaffId = Data.StaffId


        };

        var res = await _room.Create(toCreateRoom);

        return StatusCode(StatusCodes.Status201Created, res.asDto);
    }

    [HttpPut("{id}")]
    // public async Task<ActionResult> Update([FromRoute] int id,
    // [FromBody] UpdateRoomDTO Data)
    // {
    //     var existing = await _room.GetById(id);
    //     if (existing is null)
    //         return NotFound("No Room found with given Room Id");

    //     var toUpdateRoom = existing with
    //     {

    //         // UserName = Data.UserName ?? existing.UserName,
    //         // Address = Data.Address ?? existing.Address,
    //         // Bio = Data.Bio ?? existing.Bio,
    //         // Email = Data.Email ?? existing.Email



    //     };

    //     // var didUpdate = await _room.Update(toUpdateRoom);

    //     if (!didUpdate)
    //         return StatusCode(StatusCodes.Status500InternalServerError, "Could not update user");

    //     return NoContent();
    // }

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
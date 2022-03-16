using Microsoft.AspNetCore.Mvc;
using HotelTask.Models;
using HotelTask.Repositories;
using HotelTask.DTOs;

namespace HotelTask.Controllers;

[ApiController]
[Route("api/schedule")]
public class ScheduleController : ControllerBase
{
    private readonly ILogger<ScheduleController> _logger;
    private readonly IScheduleRepository _schedule;
    private readonly IGuestRepository _guest;
    private readonly IRoomRepository _room;
    public ScheduleController(ILogger<ScheduleController> logger, IScheduleRepository schedule, IGuestRepository guest, IRoomRepository room)
    {
        _logger = logger;
        _schedule = schedule;
        _guest = guest;
        _room = room;
    }

    [HttpGet]
    public async Task<ActionResult> GetList()
    {
        var res = await _schedule.GetList();

        return Ok(res.Select(x => x.asDto));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById([FromRoute] int id)
    {
        var res = await _schedule.GetById(id);

        if (res is null)
            return NotFound();
        var dto = res.asDto;
        dto.Guests = (await _guest.GetScheduleByGuestId(id))
                        .Select(x => x.asDto).ToList();
        dto.Rooms = (await _room.GetListByScheduleId(id)).Select(x => x.asDto).ToList();


        return Ok(dto);

    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateScheduleDTO Data)
    {
        var toCreateSchedule = new Schedule
        {

            CheckIn = Data.CheckIn.UtcDateTime,
            CheckOut = Data.CheckOut.UtcDateTime,
            GuestCount = Data.GuestCount,
            Price = Data.Price,
            GuestId = Data.GuestId,
            RoomId = Data.RoomId

        };

        var res = await _schedule.Create(toCreateSchedule);

        return StatusCode(StatusCodes.Status201Created, res.asDto);
    }



    [HttpPut("{id}")]
    public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateScheduleDTO Data)
    {
        var existing = await _schedule.GetById(id);

        if (existing == null)
            return NotFound();

        var toUpdateSchedule = existing with
        {
            CheckIn = Data.CheckIn.UtcDateTime,
            CheckOut = Data.CheckOut.UtcDateTime,
            GuestCount = Data.GuestCount ?? existing.GuestCount,
            Price = Data.Price ?? existing.Price
        };

        var didUpdate = await _schedule.Update(toUpdateSchedule);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        var existing = await _schedule.GetById(id);
        if (existing is null)
            return NotFound("No Schedule found with given Id");

        var didDelete = await _schedule.Delete(id);

        return NoContent();
    }
}

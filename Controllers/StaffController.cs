using Microsoft.AspNetCore.Mvc;
using HotelTask.Models;
using HotelTask.Repositories;
using HotelTask.DTOs;

namespace HotelTask.Controllers;

[ApiController]
[Route("api/staff")]
public class StaffController : ControllerBase
{
    private readonly ILogger<StaffController> _logger;
    private readonly IStaffRepository _staff;

    public StaffController(ILogger<StaffController> logger, IStaffRepository staff)
    {
        _logger = logger;
        _staff = staff;
    }

    [HttpGet]
    public async Task<ActionResult> GetList()
    {
        var res = await _staff.GetList();

        return Ok(res.Select(x => x.asDto));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById([FromRoute] int id)
    {
         var res = await _staff.GetById(id);

        if (res == null)
            return NotFound("No Staff found with given Staff Id");

        var dto = res.asDto;
        // dto.Schedules = (await _schedule.GetListByGuestId(id))
        //                 .Select(x => x.asDto).ToList();
        // dto.Rooms = (await _room.GetListByGuestId(id)).Select(x => x.asDto).ToList();

        return Ok(dto);
    }

    [HttpPost]
     public async Task<ActionResult> Create([FromBody] StaffCreateDTO Data)
    {
        var toCreateStaff = new Staff
        {
           
            DateOfBirth = Data.DateOfBirth.UtcDateTime,
            Gender = Data.Gender,
            Mobile = Data.Mobile,
            Name = Data.Name.Trim(),
            Shift = Data.Shift
        };

        var res = await _staff.Create(toCreateStaff);

        return StatusCode(StatusCodes.Status201Created, res.asDto);
    }

    [HttpPut("{id}")]
     public async Task<ActionResult> Update([FromRoute] int id, [FromBody] StaffUpdateDTO Data)
    {
        var existingStaff = await _staff.GetById(id);

        if (existingStaff == null)
            return NotFound("No Staff found with given Staff Id");

        var toUpdateStaff = existingStaff with
        {
            Name = Data.Name.Trim()  ?? existingStaff.Name,
            DateOfBirth = Data.DateOfBirth.UtcDateTime,
            Mobile = Data.Mobile ,
            Gender =Data.Gender.Trim(),
            Shift =Data.Shift.Trim() ?? existingStaff.Shift
           
        };

        var didUpdate = await _staff.Update(toUpdateStaff);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError);

        return NoContent();
    }

    // [HttpDelete("{id}")]
    // public async Task<ActionResult> Delete([FromRoute] int id)
    // {

    // }
}

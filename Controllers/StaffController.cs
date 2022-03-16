using Microsoft.AspNetCore.Mvc;
using HotelTask.Models;
using HotelTask.Repositories;

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
         var res = await _staff.GetList();

        return Ok(res.Select(x => x.asDto));
    }

    // [HttpPost]
    // public async Task<ActionResult> Create([FromRoute] int id)
    // {

    // }

    // [HttpPut("{id}")]
    // public async Task<ActionResult> Update([FromRoute] int id)
    // {

    // }

    // [HttpDelete("{id}")]
    // public async Task<ActionResult> Delete([FromRoute] int id)
    // {

    // }
}

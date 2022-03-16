using Microsoft.AspNetCore.Mvc;
using HotelTask.Models;

namespace HotelTask.Controllers;

[ApiController]
[Route("api/schedule")]
public class ScheduleController : ControllerBase
{
    private readonly ILogger<ScheduleController> _logger;

    public ScheduleController(ILogger<ScheduleController> logger)
    {
        _logger = logger;
    }

    // [HttpGet]
    // public async Task<ActionResult> GetList()
    // {

    // }

    // [HttpGet("{id}")]
    // public async Task<ActionResult> GetById([FromRoute] int id)
    // {

    // }

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

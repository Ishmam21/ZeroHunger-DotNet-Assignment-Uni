using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppLayerAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CollectRequestController : ControllerBase
{
    CollectRequestService service;

    public CollectRequestController(CollectRequestService service)
    {
        this.service = service;
    }

    [HttpGet("all")]
    public IActionResult GetAll()
    {
        return Ok(service.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var data = service.Get(id);
        if (data == null)
            return NotFound();
        return Ok(data);
    }

    [HttpPost("create")]
    public IActionResult Create(CollectRequestModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var success = service.Create(model);
        return Ok(success);
    }

    [HttpPut("update")]
    public IActionResult Update(CollectRequestModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var success = service.Update(model);
        return Ok(success);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var success = service.Delete(id);
        return Ok(success);
    }

    // workflow-specific endpoints
    [HttpPut("{id}/accept")]
    public IActionResult Accept(int id, [FromBody] int empId)
    {
        var success = service.Accept(id, empId);
        return Ok(success);
    }

    [HttpPut("{id}/collect")]
    public IActionResult MarkCollected(int id)
    {
        var success = service.MarkCollected(id);
        return Ok(success);
    }

    [HttpPut("{id}/complete")]
    public IActionResult Complete(int id)
    {
        var success = service.Complete(id);
        return Ok(success);
    }

    [HttpPut("{id}/reject")]
    public IActionResult Reject(int id)
    {
        var success = service.Reject(id);
        return Ok(success);
    }

    [HttpGet("status/{status}")]
    public IActionResult GetByStatus(string status)
    {
        return Ok(service.GetByStatus(status));
    }

    [HttpGet("restaurant/{resid}")]
    public IActionResult GetByRestaurant(int resid)
    {
        return Ok(service.GetByRestaurant(resid));
    }

    [HttpGet("employee/{empid}")]
    public IActionResult GetByEmployee(int empid)
    {
        return Ok(service.GetByEmployee(empid));
    }

    [HttpGet("urgent")]
    public IActionResult GetUrgent()
    {
        return Ok(service.GetUrgent());
    }
}
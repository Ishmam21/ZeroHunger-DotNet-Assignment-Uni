using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppLayerAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    EmployeeService service;
    public EmployeeController(EmployeeService service)
    {
        this.service = service;
    }
    [HttpGet("all")]
    public IActionResult GetAll()
    {
        var data = service.GetAll();
        return Ok(data);
    }

    [HttpGet("get/{id}")]
    public IActionResult Get(int id)
    {
        var data = service.Get(id);
        return Ok(data);
    }

    [HttpPost("create")]
    public IActionResult Create(EmployeeModel model)
    {
        var data = service.Create(model);
        return Ok(data);
    }

    [HttpPut("update")]
    public IActionResult Update(EmployeeModel model)
    {
        var data = service.Update(model);
        return Ok(data);
    }

    [HttpDelete("delete/{id}")]
    public IActionResult Delete(int id)
    {
        var data = service.Delete(id);
        return Ok(data);
    }
}

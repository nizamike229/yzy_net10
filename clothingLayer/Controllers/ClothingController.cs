using clothingLayer.Contracts;
using clothingLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace clothingLayer.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ClothingController : ControllerBase
{
    private readonly IClothingService _service;

    public ClothingController(IClothingService service)
    {
        _service = service;
    }

    [HttpGet]
    [ActionName("get")]
    public async Task<ActionResult<List<ClothingResponse>>> GetAll()
    {
        try
        {
            return Ok(await _service.GetClothingsAsync());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [ActionName("getById")]
    public async Task<ActionResult<ClothingResponse>> GetById([FromQuery] string id)
    {
        try
        {
            return Ok(await _service.GetByIdAsync(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [ActionName("add")]
    public async Task<ActionResult<object>> Add([FromBody] ClothingRequest clothingRequest)
    {
        try
        {
            await _service.CreateClothingAsync(clothingRequest);
            return Ok(new { Message = "Successfully added clothing" });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [ActionName("delete")]
    public async Task<ActionResult<object>> Delete([FromQuery] string id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return Ok(new { Message = "Successfully deleted clothing" });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
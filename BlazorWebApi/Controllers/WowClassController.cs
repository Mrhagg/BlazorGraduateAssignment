using BlazorWebApi.Dtos;
using BlazorWebApi.Interface;
using BlazorWebApi.Services;
using Microsoft.AspNetCore.Mvc;


namespace BlazorWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WowClassController : ControllerBase
{
    private readonly IWowClassService _wowClassService;

    public WowClassController(IWowClassService wowClassService)
    {
        _wowClassService = wowClassService;
    }

    [HttpGet("wowclass/{classId}/specializations")]
    public async Task<ActionResult<List<SpecializationDto>>> GetSpecs(int classId)
    {
        var result = await _wowClassService.GetSpecializationsByClassIdAsync(classId);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<List<WowClassDto>>> GetAll()
    {
        var result = await _wowClassService.GetAllWowClassesAsync();
        return Ok(result);
    }

    [HttpGet("races/{raceId}/classes")]
    public async Task<ActionResult<List<WowClassDto>>> GetClassesByRaceId(int raceId)
    {
        var classes = await _wowClassService.GetClassesByRaceIdAsync(raceId);
        return Ok(classes);
    }

}

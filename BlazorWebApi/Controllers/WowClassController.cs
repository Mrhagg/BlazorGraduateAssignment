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
    private readonly ISpecializationService specializationService;
    public WowClassController(IWowClassService wowClassService, ISpecializationService _specializationService)
    {
        _wowClassService = wowClassService;
        specializationService = _specializationService; 
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

    [HttpGet("{id}/specializations")]
    public async Task<ActionResult<List<SpecializationDto>>> GetSpecializationsForClass(int id)
    {
        var result = await specializationService.GetSpecializationsByClassIdAsync(id);
        return Ok(result);
    }

}

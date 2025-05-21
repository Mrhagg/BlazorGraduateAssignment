using BlazorWebApi.Dtos;
using BlazorWebApi.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SpecializationsController : ControllerBase
{
    private readonly ISpecializationService _specializationService;

    public SpecializationsController(ISpecializationService specializationService)
    {
        _specializationService = specializationService;
    }

    [HttpGet]
    public async Task <ActionResult<List<SpecializationDto>>> GetAll()
    {
        var result = await _specializationService.GetAllSpecializationsAsync();
        return Ok(result);
    }
}

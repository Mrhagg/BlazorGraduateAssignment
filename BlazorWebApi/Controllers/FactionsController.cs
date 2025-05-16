using BlazorWebApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BlazorWebApi.Dtos;

namespace BlazorWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FactionsController : ControllerBase
{
    private readonly ApplicationDbContext context;
    public FactionsController(ApplicationDbContext context)
    {
        this.context = context;
    }


    [HttpGet]
    public async Task<IActionResult> GetFactions()
    {
        var factions = await context.Factions
            .Select(f => new
            {
                f.Id,
                f.Name,
                f.ImageUrl
            }).ToListAsync();

        return Ok(factions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FactionsDto>> GetFactionById(int id)
    {
        var faction = await context.Factions
            .Where(f => f.Id == id)
            .Select(f => new FactionsDto
            {
                Id = f.Id,
                Name = f.Name,
                ImageUrl = f.ImageUrl
            }).FirstOrDefaultAsync();

        if (faction == null)
            return NotFound();

        return Ok(faction);
    }



    [HttpGet("{factionId}/races")]
    public async Task<ActionResult<List<RaceDto>>> GetRacesByFaction(int factionId)
    {
        var factionExists = await context.Factions.AnyAsync(f => f.Id == factionId);
        if (!factionExists)
            return NotFound($"Faction with Id {factionId} not found.");

        var races = await context.CharacterRaces
        .Where(r => r.FactionId == factionId)
        .Include(r => r.RaceWowClasses)
            .ThenInclude(rwc => rwc.WowClass)
        .ToListAsync();

        var raceDtos = races.Select(r => new RaceDto
        {
            Id = r.Id,
            Name = r.Name,
            Description = r.Description,
            ImageUrl = r.ImageUrl,
            AllowedClasses = r.RaceWowClasses.Select(rwc => new WowClassDto
            {
                Id = rwc.WowClass.Id,
                Name = rwc.WowClass.Name
            }).ToList()

        }).ToList();

        if (races == null || races.Count == 0)
            return NotFound();

        return Ok(raceDtos);
    }

}

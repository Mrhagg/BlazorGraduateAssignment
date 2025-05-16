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


    [HttpGet("{id}/races")]
    public async Task<ActionResult<List<RaceDto>>> GetRacesByFaction(int factionId)
    {
        var factionExists = await context.Factions.AnyAsync(f => f.Id == factionId);
        if (!factionExists)
            return NotFound($"Faction with Id {factionId} not found.");

        var races = await context.CharacterRaces.Where(r => r.FactionId == factionId).Include(r => r.RaceWowClasses).ThenInclude(rwc => rwc.WowClass).Select(r => new
        {
            r.Id,
            r.Name,
            r.Description,
            Classes = r.RaceWowClasses.Select(rwc => rwc.WowClass.Name),
        }).ToListAsync();

        if (races == null || races.Count == 0)
            return NotFound();

        return Ok(races);
    }

}

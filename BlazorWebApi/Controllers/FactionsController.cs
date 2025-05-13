using BlazorWebApi.Models;
using BlazorWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    public List<Factions> GetFactions()
    {
        return context.Factions.OrderByDescending(f => f.Id).ToList();
    }

    [HttpGet("{id}")]
    public IActionResult GetFaction(int id)
    {
        var faction = context.Factions.Find(id);
        if(faction == null)
        {
            return NotFound();
        }

        return Ok(faction);
    }

    
}

using BlazorWebApi.Dtos;
using BlazorWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TalenTreeController : ControllerBase
{

    private readonly ApplicationDbContext _context;

    public TalenTreeController(ApplicationDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<ActionResult<TalentTreeDto>> GetTalentTree()
    {
        var root = new TalentTreeDto
        {
            Id = 1,
            Name = "Specialization tree",
            Description = "Choose your talents",
            IconUrl = "https://example.com/icon.png",
            IsUnlocked = true,
            Children = new List<TalentTreeDto>
            {
                new TalentTreeDto
                {
                    Id = 2,
                    Name = "Talent 1",
                    Description = "Description of talent 1",
                    IconUrl = "https://example.com/talent1.png",
                    IsUnlocked = true,
                    Children = new List<TalentTreeDto>()
                },
                new TalentTreeDto
                {
                    Id = 3,
                    Name = "Talent 2",
                    Description = "Description of talent 2",
                    IconUrl = "https://example.com/talent2.png",
                    IsUnlocked = false,
                    Children = new List<TalentTreeDto>()
                }
            }
        };
        return Ok(new List<TalentTreeDto> { root });
    }

    [HttpGet("specialization/{specializationId}")]
    public async Task<ActionResult<List<TalentTreeDto>>> GetBySpecialization(int specializationId)
    {
        var allNodes = await _context.TalentTree.Where(t => t.SpecializationId == specializationId)
            .ToListAsync();

        var lookup = allNodes.ToLookup(t => t.ParentId);

        List<TalentTreeDto> BuildTree(int? parentId)
        {
            return lookup[parentId].Select(t => new TalentTreeDto
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                IconUrl = t.IconUrl,
                IsUnlocked = t.IsUnlocked,
                Children = BuildTree(t.Id)
            }).ToList();
        }

        var tree = BuildTree(null);

        return Ok(tree);
    }
   
}

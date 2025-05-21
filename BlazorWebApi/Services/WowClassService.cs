using BlazorWebApi.Dtos;
using BlazorWebApi.Interface;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebApi.Services;

public class WowClassService : IWowClassService
{
    private readonly ApplicationDbContext _context;

    public WowClassService(ApplicationDbContext context)
    {
        _context = context;
    }

   
    public async Task<List<SpecializationDto>> GetAllSpecializationsAsync()
    {
        return await _context.Specializations.Select(s => new SpecializationDto
        {
            Id = s.Id,
            Name = s.Name,
            RoleName = s.Role.Name,
            DamageType = GetDamageType(s.Name)
        }).ToListAsync();
    }

    public async Task<List<WowClassDto>> GetAllWowClassesAsync()
    {
        var classes = await _context.WowClasses.Include(c => c.RaceWowClasses).ToListAsync();

        return await _context.WowClasses.Select(c => new WowClassDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            ImageFileName = c.ImageUrl,
            Races = c.RaceWowClasses.Select(rw => rw.CharacterRace.Name).ToList(),
            Specializations = c.Specializations.Select(s => new SpecializationDto
            {
                Id = s.Id,
                Name = s.Name,
                RoleName = s.Role.Name,
                DamageType = GetDamageType(s.Name)
            }).ToList()
        }).ToListAsync();
    }

    public async Task<List<WowClassDto>> GetClassesByRaceIdAsync(int raceId)
    {
            var classes = await _context.RaceWowClasses
        .Where(rc => rc.CharacterRaceId == raceId)
        .Select(rc => rc.WowClass)
        .Select(c => new WowClassDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            ImageFileName = c.ImageUrl,
            Specializations = c.Specializations.Select(s => new SpecializationDto
            {
                Id = s.Id,
                Name = s.Name,
                RoleName = s.Role.Name,
                DamageType = GetDamageType(s.Name)
            }).ToList()
        }).ToListAsync();

            return classes;

    }

    public async Task<List<SpecializationDto>> GetSpecializationsByClassIdAsync(int classId)
    {
        return await _context.Specializations.Where(s => s.WowClassId == classId).Select(s => new SpecializationDto
        {
            Id = s.Id,
            Name = s.Name,
            RoleName = s.Role.Name,
            DamageType = GetDamageType(s.Name)
        }).ToListAsync();
    }


    private static string? GetDamageType(string? specName)
    {
        if (string.IsNullOrWhiteSpace(specName)) return null;

        var meleeSpecs = new[] {
            "Fury", "Arms", "Retribution", "Windwalker", "Feral", "Survival", "Havoc", "Frost", "Unholy",
            "Outlaw", "Subtlety", "Assassination", "Enhancement", "Brewmaster", "Blood", "Vengeance"
        };

        var rangedSpecs = new[] {
            "Beast Mastery", "Marksmanship", "Balance", "Shadow", "Frost", "Fire", "Arcane",
            "Destruction", "Demonology", "Affliction", "Devastation", "Augmentation", "Elemental",
            "Restoration", "Preservation", "Discipline", "Holy"
        };

        if (meleeSpecs.Contains(specName)) return "Melee";
        if (rangedSpecs.Contains(specName)) return "Ranged";

        return "Unknown";
    }
}

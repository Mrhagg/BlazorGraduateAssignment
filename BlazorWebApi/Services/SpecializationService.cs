using BlazorWebApi.Dtos;
using BlazorWebApi.Interface;
using BlazorWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebApi.Services;

public class SpecializationService : ISpecializationService
{
    private readonly ApplicationDbContext _context;

    public SpecializationService(ApplicationDbContext context)
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

    public async Task<SpecializationDto?> GetSpecializationByIdAsync(int id)
    {
        return await _context.Specializations
            .Where(s => s.Id == id)
            .Select(s => new SpecializationDto
            {
                Id = s.Id,
                Name = s.Name,
                RoleName = s.Role.Name,
                DamageType = GetDamageType(s.Name)
            }).FirstOrDefaultAsync();
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
    
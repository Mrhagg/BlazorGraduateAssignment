using BlazorWebApi.Interface;
using BlazorWebApi.Models;
using BlazorWebApi.Services;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebApi.Repositorys;

public class CharacterRaceRepository : ICharacterRepository
{
    private readonly ApplicationDbContext context;

    public CharacterRaceRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<CharacterRace> GetById(int id)
    {
        return await context.CharacterRaces
            .Include(r => r.Faction)
            .Include(r => r.RaceWowClasses)
            .ThenInclude(rwc => rwc.WowClass)
            .FirstOrDefaultAsync(r => r.Id == id);
    }
}

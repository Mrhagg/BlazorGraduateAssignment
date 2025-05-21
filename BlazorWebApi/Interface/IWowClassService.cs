using BlazorWebApi.Dtos;

namespace BlazorWebApi.Interface;

public interface IWowClassService
{
    Task<List<WowClassDto>> GetAllWowClassesAsync();

    Task<List<SpecializationDto>> GetAllSpecializationsAsync();

    Task<List<SpecializationDto>> GetSpecializationsByClassIdAsync(int classId);

    Task<List<WowClassDto>> GetClassesByRaceIdAsync(int raceId);
}

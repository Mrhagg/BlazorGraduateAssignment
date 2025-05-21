using BlazorWebApi.Dtos;

namespace BlazorWebApi.Interface;

public interface ISpecializationService
{
    Task<List<SpecializationDto>> GetAllSpecializationsAsync();
    Task<List<SpecializationDto>> GetSpecializationsByClassIdAsync(int classId);
    Task<SpecializationDto?> GetSpecializationByIdAsync(int id);
}

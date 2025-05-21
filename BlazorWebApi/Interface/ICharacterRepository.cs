using BlazorWebApi.Models;

namespace BlazorWebApi.Interface;

public interface ICharacterRepository 
{
    Task<CharacterRace> GetById(int id);
}

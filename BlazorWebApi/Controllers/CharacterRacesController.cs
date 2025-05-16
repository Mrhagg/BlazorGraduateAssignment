using BlazorWebApi.Dtos;
using BlazorWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CharacterRacesController : ControllerBase
{
    private readonly ApplicationDbContext context;

    public CharacterRacesController(ApplicationDbContext context)
    {
        this.context = context;
    }


    [HttpPost("UpdateImages")]
    public async Task <IActionResult> UpdateCharacterRaceImages()
    {
        string imagesFolder = "images";

        var imageFiles = new List<string>
        {
            "Bloodelf.jpg",
            "DarkIronDwarf.jpg",
            "Drachtyr.jpg",
            "Dranei.jpg",
            "Dwarf.jpg",
            "Earthen.jpg",
            "Gnome.jpg",
            "Goblin.jpg",
            "Highmountain.jpg",
            "Human.jpg",
            "Maghar.jpg",
            "Nightborne.jpg",
            "Orc.jpg",
            "Pandaren.jpg",
            "Tauren.jpg",
            "Troll.jpg",
            "Undead.jpg",
            "Vulpera.jpg",
            "Zandalari.jpg"
        };

        var races = await context.CharacterRaces.ToListAsync();

        foreach (var race in races)
        {
            var matchedFiles = imageFiles.FirstOrDefault(f => Path.GetFileNameWithoutExtension(f).Equals(race.Name, System.StringComparison.OrdinalIgnoreCase));

            if (matchedFiles != null)
            {
                race.ImageUrl = matchedFiles;
            }
           
        }

        await context.SaveChangesAsync();

        return Ok(new { message = "Images updated successfully." });
    }

    [HttpPost("UpdateDescriptions")]
    public async Task<IActionResult> UpdateDescriptionsAndImageUrl()
    {
        var raceDescriptions = new Dictionary<int, string>
{
    { 6, "Eleganta magiker och krigare som återuppbyggt sitt rike efter katastrofen." }, 
    { 7, "Starka krigare med stolta traditioner från Durotars öknar." },                
    { 8, "Placeholder description" },                                                 
    { 9, "Fredliga jägare och naturälskare med stark koppling till jorden." },        
    { 10, "Smarta och anpassningsbara ökenvandrare, kända för sitt handelssinne." },    
    { 11, "Listiga jägare med starka shamanistiska och voodoo-traditioner." },          
    { 12, "Mäktiga och stolta troll med urgamla kungariken och starka magiska krafter." }, 
    { 13, "Odöda som brutit sin förbannelse och strider för sin frihet." },            
    { 14, "Listiga och tekniskt avancerade handelsmän och uppfinnare." },               
    { 15, "Tauren med bergsstammar som värnar sin kultur och tradition." },           
    { 16, "Titanformade varelser, starka och tåliga med en uråldrig koppling till Azeroth." }, 
    { 17, "Magiskt begåvade nattvarelser med en rik historia och stolthet." },        
    { 18, "Drakliknande krigare med magiska och fysiska förmågor, ny och kraftfull ras." }, 
    { 19, "Fredliga upptäcktsresande med visdom och balans, älskade av båda fraktionerna." }, 
    { 20, "Hårdföra dvärgar från underjorden med en brinnande ilska och styrka." },     
    { 21, "Drakliknande krigare med magiska och fysiska förmågor, ny och kraftfull ras." }, 
    { 22, "Fridfulla och spirituella utomjordingar med stark magisk koppling." },      
    { 23, "Tuffa bergsbor och skickliga hantverkare med djup stolthet för sin historia." }, 
    { 24, "Titanformade varelser, starka och tåliga med en uråldrig koppling till Azeroth." }, 
    { 25, "Nyfikna och tekniskt begåvade småuppfinnare och äventyrare." },             
    { 26, "Mångsidiga och uthålliga med stark känsla för rättvisa och mod." },          
    { 27, "Sjöfarare och krigare från kustregionen med starka band till havet och naturen." }, 
    { 28, "Heliga krigare med stark koppling till ljusets krafter." },                 
    { 29, "Teknologiskt avancerade gnomer med mekaniska kroppar och hög intelligens." }, 
    { 30, "Mystiska skogsvarelser som skyddar naturens balans med starka magiska krafter." }, 
    { 31, "Fredliga upptäcktsresande med visdom och balans, älskade av båda fraktionerna." }, 
    { 32, "Magiskt kraftfulla alver som balanserar mellan voidens krafter och sitt förflutna." }, 
    { 33, "Människor förbannade att anta vargform, kombinerar styrka och smidighet." }, 
};

        var raceImages = new Dictionary<int, string>
{
    { 7,  "Orc.jpg" },
    { 9,  "Tauren.jpg" },
    { 10, "Vulpera.jpg" },
    { 11, "Troll.jpg" },
    { 13, "Undead.jpg" },
    { 14, "Goblin.jpg" },
    { 16, "Earthen.jpg" },
    { 17, "Nightborne.jpg" },
    { 19, "Pandaren.jpg" },
    { 23, "Dwarf.jpg" },
    { 24, "Earthen.jpg" },
    { 25, "Gnome.jpg" },
    { 26, "Human.jpg" },
    { 31, "Pandaren.jpg" },
};

        var races = await context.CharacterRaces.ToListAsync();

        foreach (var race in races)
        {
            if (raceDescriptions.TryGetValue(race.Id, out var description))
            {
                race.Description = description;
            }

            if (raceImages.TryGetValue(race.Id, out var image))
            {
                race.ImageUrl = "/images/" + image; 
            }
            else
            {
                race.ImageUrl = null; 
            }
        }


        await context.SaveChangesAsync();

        return Ok(new { message = "Description updated successfully!"});
    }

    [HttpGet]
    public async Task<ActionResult<List<RaceDto>>> Get()
    {
        var races = await context.CharacterRaces.ToListAsync();

        
        var dtos = races.Select(r => new RaceDto
        {
            Id = r.Id,
            Name = r.Name,
            Description = r.Description,
            ImageUrl = r.ImageUrl
        }).ToList();

        return Ok(dtos);
    }

}

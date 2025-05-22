using BlazorWebApi.Dtos;
using BlazorWebApi.Interface;
using BlazorWebApi.Repositorys;
using BlazorWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CharacterRacesController : ControllerBase
{

    private readonly ApplicationDbContext context;
    private readonly ICharacterRepository characterRepository;

    public CharacterRacesController(ApplicationDbContext context, ICharacterRepository characterRepository)
    {
        this.context = context;
        this.characterRepository = characterRepository;
    }

  

    [HttpPost("UpdateDescriptions")]
    public async Task<IActionResult> UpdateDescriptionsAndImageUrl()
    {
            var raceDescriptions = new Dictionary<int, string>
        {
            { 6, "Former members of the Alliance, these elves turned to the Horde after betrayal and tragedy. They are elegant, proud, and powerful mages." }, 
            { 7, "A warrior race from Draenor, orcs are known for their strength, honor, and fierce loyalty. They have found a new home in Azeroth under the Horde." },                
            { 8, "Uncorrupted orcs from an alternate Draenor timeline who never succumbed to demonic influence." },                                                 
            { 9, "Massive, bovine-like creatures who revere nature and the Earth Mother. They value balance and spiritual harmony." },        
            { 10, "Cunning, fox-like nomads from Vol'dun who excel in survival, resourcefulness, and exploration." },    
            { 11, "Agile and strong, the Darkspear trolls are fierce warriors with a deep spiritual culture and ties to voodoo practices." },          
            { 12, "A mighty troll empire with a deep cultural legacy and strong ties to the loa spirits." }, 
            { 13, "Undead former people who are fighting for their freedom as their forsaken heritage." },            
            { 14, "Small, greedy, and ingenious, goblins thrive on trade, explosions, and clever inventions. Always looking for the next profit or opportunity." },               
            { 15, "A proud tribe of tauren from the Broken Isles with unique antlers and mountain traditions." },           
            { 16, "Titan dwarf beigns with their home in the heart of Azeroth." }, 
            { 17, "Elves from Suramar who broke free of their magical isolation and now fight for the Horde.\r\n\r\n" },        
            { 18, "Dragon humanoide creatures with a special set of warrior skills from the dragon isles." }, 
            { 19, "Peaceful and wise, pandaren hail from the mists of Pandaria. After leaving their homeland, they can choose to join either the Alliance or the Horde." }, 
            { 20, "A once-hostile dwarf clan now united with the Alliance, known for their fiery tempers and craftsmanship." },     
            { 21, "Dragon humanoide creatures with a special set of warrior skills from the dragon isles." }, 
            { 22, "A noble race with a deep connection to the Light. The draenei fled their corrupted homeworld and now fight alongside the Alliance." },      
            { 23, "Stout and sturdy, dwarves are master craftsmen and archaeologists. They hail from snowy mountains and are known for their bravery and loyalty." }, 
            { 24, "Titan dwarf beigns with their home in the heart of Azeroth." }, 
            { 25, "Small in stature but brilliant in intellect, gnomes are technological geniuses. Their inventions are both ingenious and often dangerous." },             
            { 26, "Courageous and adaptable, humans are known for their resilience and determination. They play a central role in the Alliance and have a strong presence across Azeroth." },          
            { 27, "Hardy seafarers with a strong connection to druidic traditions and the sea." }, 
            { 28, "Holy warriors who have undergone intense training to become even more attuned to the Light." },                 
            { 29, "Gnomes who have augmented their bodies with mechanical enhancements in pursuit of perfection." }, 
            { 30, "An ancient race in tune with nature, night elves are mystical guardians of the forests. They are agile warriors, skilled hunters, and powerful druids." }, 
            { 31, "Peaceful and wise, pandaren hail from the mists of Pandaria. After leaving their homeland, they can choose to join either the Alliance or the Horde." }, 
            { 32, "Former blood elves who now harness the powers of the Void to serve the Alliance." }, 
            { 33, "Once human citizens of Gilneas, these cursed beings transform into werewolf-like creatures. Despite their curse, they fight with fierce loyalty." }, 
        };

     

        var races = await context.CharacterRaces.ToListAsync();

        foreach (var race in races)
        {
            if (raceDescriptions.TryGetValue(race.Id, out var description))
            {
                race.Description = description;
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
            ImageFileName = r.ImageFileName
        }).ToList();

        return Ok(dtos);
    }

    [HttpGet("{id}/RaceImages")]
    public async Task<IActionResult> GetImageById(int id)
    {
        var race = await context.CharacterRaces.FindAsync(id);
        if(race == null)
            return NotFound();

        var dto = new RaceDto
        {
            Id = race.Id,
            Name = race.Name,
            Description = race.Description,
            ImageFileName = race.ImageFileName
        };

        return Ok(dto);
    }

    [HttpGet("{id}")]
    public async Task <ActionResult<RaceDto>> GetRaceById(int id)
    {
       var race = await characterRepository.GetById(id);
        if (race == null)
        {
            return NotFound();
        }

        var raceDto = new RaceDto
        {
            Id = race.Id,
            Name = race.Name,
            Description = race.Description,
            ImageFileName = race.ImageFileName,
            RacialAbilities = race.RacialAbilities,
            AllowedClasses = race.RaceWowClasses?.Select(rwc => new WowClassDto
            {
                Id = rwc.WowClassId,
                Name = rwc.WowClass.Name,
            }).ToList()

        };

        return Ok(raceDto);
    }

    [HttpGet("{id}/classes")]
    public async Task<ActionResult<List<WowClassDto>>> GetAllowedClasses(int id)
    {
        var race = await characterRepository.GetById(id);
        if (race == null)
            return NotFound();

        var allowedClasses = race.RaceWowClasses?
            .Where(rwc => rwc.WowClass != null) 
            .Select(rwc => new WowClassDto
            {
                Id = rwc.WowClassId,
                Name = rwc.WowClass.Name
            }).ToList();

        return Ok(allowedClasses);
    }



    [HttpPost("updateracialabilities")]
    public async Task<IActionResult> UpdateRacialAbilities()
    {
        var races = await context.CharacterRaces.ToListAsync();

        foreach (var race in races)
        {
            switch (race.Id)
            {
                case 6: 
                    race.RacialAbilities = "Arcane Torrent: Silences enemies and restores resources.";
                    break;
                case 7: 
                    race.RacialAbilities = "Blood Fury: Boosts attack power. Hardiness: Reduced stun duration.";
                    break;
                case 8: 
                    race.RacialAbilities = "Ancestral Call: Boosts secondary stat. Hardiness: Reduced stun duration.";
                    break;
                case 9: 
                    race.RacialAbilities = "War Stomp: AoE stun. Endurance: Increased health.";
                    break;
                case 10: 
                    race.RacialAbilities = "Bag of Tricks: Use a trick for damage or healing. Make Camp: Set a return point.";
                    break;
                case 11: 
                    race.RacialAbilities = "Berserking: Boosts haste. Regeneration: Health regen during combat.";
                    break;
                case 12: 
                    race.RacialAbilities = "Regeneratin': Massive health regen. Embrace of the Loa: Choose a Loa buff.";
                    break;
                case 13: 
                    race.RacialAbilities = "Will of the Forsaken: Removes charm/fear/sleep.Cannibalize: Heal from corpses.";
                    break;
                case 14: 
                    race.RacialAbilities = "Rocket Jump: Forward leap. Time is Money: Boosts haste.";
                    break;
                case 15: 
                    race.RacialAbilities = "Bull Rush: Charge and stun. Rugged Tenacity: Reduced damage taken.";
                    break;
                case 16: 
                case 24: 
                    race.RacialAbilities = "Titanic Legacy: Increased damage resistance and strength.";
                    break;
                case 17: 
                    race.RacialAbilities = "Arcane Pulse: AoE damage and slow. Magical Affinity: Increased magic damage.";
                    break;
                case 18: 
                case 21: 
                    race.RacialAbilities = "Soar: Fly through the air. Chosen Identity: Choose visage appearance.";
                    break;
                case 19: 
                case 31: 
                    race.RacialAbilities = "Quaking Palm: Incapacitate target. Epicurean: Better food buffs.";
                    break;
                case 20: 
                    race.RacialAbilities = "Fireblood: Removes effects and boosts stats. Mole Machine: Set teleports.";
                    break;
                case 22: 
                    race.RacialAbilities = "Gift of the Naaru: HoT heal. Heroic Presence: Boosts hit chance.";
                    break;
                case 23:
                    race.RacialAbilities = "Stoneform: Removes effects and reduces damage. Might of the Mountain: Crit damage.";
                    break;
                case 25: 
                    race.RacialAbilities = "Escape Artist: Break root/snare. Expansive Mind: More resource pool.";
                    break;
                case 26: 
                    race.RacialAbilities = "Will to Survive: Removes stun. The Human Spirit: Bonus to secondary stats.";
                    break;
                case 27: 
                    race.RacialAbilities = "Haymaker: Knockback and stun. Brush It Off: Passive self-healing.";
                    break;
                case 28: 
                    race.RacialAbilities = "Light's Judgment: AoE holy damage. Demonbane: Bonus vs. demons.";
                    break;
                case 29: 
                    race.RacialAbilities = "Combat Analysis: Stat stacking in combat. Emergency Failsafe: Self-heal.";
                    break;
                case 30: 
                    race.RacialAbilities = "Shadowmeld: Stealth while standing. Quickness: Increased dodge chance.";
                    break;
                case 32: 
                    race.RacialAbilities = "Spatial Rift: Blink-like teleport. Entropic Embrace: Bonus shadow damage/healing.";
                    break;
                case 33: 
                    race.RacialAbilities = "Darkflight: Short sprint. Viciousness: Bonus crit chance.";
                    break;
                default:
                    race.RacialAbilities = "Unknown";
                    break;
            }
        }

        await context.SaveChangesAsync();
        return Ok("Racial abilities updated successfully.");
    }

}

using BlazorWebApi.Models;
using BlazorWebApi.Services;
using Microsoft.EntityFrameworkCore;

public static class DbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        var existingClassNames = await context.WowClasses.Select(c => c.Name).ToListAsync();
        var existingRoleNames = await context.Roles.Select(r => r.Name).ToListAsync();

        Roles damageRole = await GetOrCreateRoleAsync(context, "Damage", existingRoleNames);
        Roles tankRole = await GetOrCreateRoleAsync(context, "Tank", existingRoleNames);
        Roles healerRole = await GetOrCreateRoleAsync(context, "Healer", existingRoleNames);

        await context.SaveChangesAsync();

        var allClasses = new List<WowClass>
        {
            new WowClass
            {
                Name = "Death Knight",
                Description = "A powerful melee fighter with dark magic.",
                ImageUrl = "deathknight.jpg",
                Specializations = new List<Specialization>
                {
                    new Specialization { Name = "Blood", Role = tankRole },
                    new Specialization { Name = "Frost", Role = damageRole },
                    new Specialization { Name = "Unholy", Role = damageRole },
                }
            },
            new WowClass
            {
                Name = "Demon Hunter",
                Description = "A swift and deadly melee fighter with demonic powers.",
                ImageUrl = "demonhunter.jpg",
                Specializations = new List<Specialization>
                {
                    new Specialization { Name = "Havoc", Role = damageRole },
                    new Specialization { Name = "Vengeance", Role = tankRole },
                }
            },
            new WowClass
            {
                Name = "Druid",
                Description = "A versatile shapeshifter and nature wielder.",
                ImageUrl = "druid.jpg",
                Specializations = new List<Specialization>
                {
                    new Specialization { Name = "Balance", Role = damageRole },
                    new Specialization { Name = "Feral", Role = damageRole },
                    new Specialization { Name = "Guardian", Role = tankRole },
                    new Specialization { Name = "Restoration", Role = healerRole },
                }
            },
            new WowClass
            {
                Name = "Evoker",
                Description = "A master of draconic magic.",
                ImageUrl = "evoker.jpg",
                Specializations = new List<Specialization>
                {
                    new Specialization { Name = "Devastation", Role = damageRole },
                    new Specialization { Name = "Preservation", Role = healerRole },
                    new Specialization { Name = "Augmentation", Role = damageRole },
                }
            },
            new WowClass
            {
                Name = "Hunter",
                Description = "A skilled ranged damage dealer with pets.",
                ImageUrl = "hunter.jpg",
                Specializations = new List<Specialization>
                {
                    new Specialization { Name = "Beast Mastery", Role = damageRole },
                    new Specialization { Name = "Marksmanship", Role = damageRole },
                    new Specialization { Name = "Survival", Role = damageRole },
                }
            },
            new WowClass
            {
                Name = "Mage",
                Description = "A powerful ranged spellcaster.",
                ImageUrl = "mage.jpg",
                Specializations = new List<Specialization>
                {
                    new Specialization { Name = "Arcane", Role = damageRole },
                    new Specialization { Name = "Fire", Role = damageRole },
                    new Specialization { Name = "Frost", Role = damageRole },
                }
            },
            new WowClass
            {
                Name = "Monk",
                Description = "A versatile martial artist.",
                ImageUrl = "monk.jpg",
                Specializations = new List<Specialization>
                {
                    new Specialization { Name = "Brewmaster", Role = tankRole },
                    new Specialization { Name = "Mistweaver", Role = healerRole },
                    new Specialization { Name = "Windwalker", Role = damageRole },
                }
            },
            new WowClass
            {
                Name = "Paladin",
                Description = "A holy warrior with healing and tanking capabilities.",
                ImageUrl = "paladin.jpg",
                Specializations = new List<Specialization>
                {
                    new Specialization { Name = "Holy", Role = healerRole },
                    new Specialization { Name = "Protection", Role = tankRole },
                    new Specialization { Name = "Retribution", Role = damageRole },
                }
            },
            new WowClass
            {
                Name = "Priest",
                Description = "A master of healing and light.",
                ImageUrl = "priest.jpg",
                Specializations = new List<Specialization>
                {
                    new Specialization { Name = "Discipline", Role = healerRole },
                    new Specialization { Name = "Holy", Role = healerRole },
                    new Specialization { Name = "Shadow", Role = damageRole },
                }
            },
            new WowClass
            {
                Name = "Rogue",
                Description = "A stealthy melee damage dealer.",
                ImageUrl = "rogue.jpg",
                Specializations = new List<Specialization>
                {
                    new Specialization { Name = "Assassination", Role = damageRole },
                    new Specialization { Name = "Outlaw", Role = damageRole },
                    new Specialization { Name = "Subtlety", Role = damageRole },
                }
            },
            new WowClass
            {
                Name = "Shaman",
                Description = "A versatile caster and melee fighter.",
                ImageUrl = "shaman.jpg",
                Specializations = new List<Specialization>
                {
                    new Specialization { Name = "Elemental", Role = damageRole },
                    new Specialization { Name = "Enhancement", Role = damageRole },
                    new Specialization { Name = "Restoration", Role = healerRole },
                }
            },
            new WowClass
            {
                Name = "Warlock",
                Description = "A master of dark magic and demons.",
                ImageUrl = "warlock.jpg",
                Specializations = new List<Specialization>
                {
                    new Specialization { Name = "Affliction", Role = damageRole },
                    new Specialization { Name = "Demonology", Role = damageRole },
                    new Specialization { Name = "Destruction", Role = damageRole },
                }
            },
            new WowClass
            {
                Name = "Warrior",
                Description = "A strong and resilient fighter.",
                ImageUrl = "warrior.jpg",
                Specializations = new List<Specialization>
                {
                    new Specialization { Name = "Arms", Role = damageRole },
                    new Specialization { Name = "Fury", Role = damageRole },
                    new Specialization { Name = "Protection", Role = tankRole },
                }
            }
        };

        foreach (var wowClass in allClasses)
        {
            if (!existingClassNames.Contains(wowClass.Name))
            {
                context.WowClasses.Add(wowClass);
            }
        }

        await context.SaveChangesAsync();
    }

    private static async Task<Roles> GetOrCreateRoleAsync(ApplicationDbContext context, string roleName, List<string> existingRoleNames)
    {
        if (existingRoleNames.Contains(roleName))
        {
            return await context.Roles.FirstAsync(r => r.Name == roleName);
        }
        else
        {
            var role = new Roles { Name = roleName };
            context.Roles.Add(role);
            await context.SaveChangesAsync();
            existingRoleNames.Add(roleName);
            return role;
        }
    }
}

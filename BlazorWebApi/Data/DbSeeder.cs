using BlazorWebApi.Dtos;
using BlazorWebApi.Models;
using BlazorWebApi.Services;
using Microsoft.EntityFrameworkCore;

public static class DbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        Console.WriteLine("SeedAsync körs...");

        
        var existingRoleNames = await context.Roles.Select(r => r.Name).ToListAsync();
        var existingClassNames = await context.WowClasses.Select(c => c.Name).ToListAsync();

        
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
                    new Specialization { Name = "Blood", RoleId = tankRole.Id },
                    new Specialization { Name = "Frost", RoleId = damageRole.Id },
                    new Specialization { Name = "Unholy", RoleId = damageRole.Id },
                }
            },
            new WowClass
            {
                Name = "Demon Hunter",
                Description = "A swift and deadly melee fighter with demonic powers.",
                ImageUrl = "demonhunter.jpg",
                Specializations = new List<Specialization>
                {
                    new Specialization { Name = "Havoc", RoleId = damageRole.Id },
                    new Specialization { Name = "Vengeance", RoleId = tankRole.Id },
                }
            },
            new WowClass
            {
                Name = "Druid",
                Description = "A versatile shapeshifter and nature wielder.",
                ImageUrl = "druid.jpg",
                Specializations = new List<Specialization>
                {
                    new Specialization { Name = "Balance", RoleId = damageRole.Id },
                    new Specialization { Name = "Feral", RoleId = damageRole.Id },
                    new Specialization { Name = "Guardian", RoleId = tankRole.Id },
                    new Specialization { Name = "Restoration", RoleId = healerRole.Id },
                }
            },
            new WowClass
            {
                Name = "Evoker",
                Description = "A master of draconic magic.",
                ImageUrl = "evoker.jpg",
                Specializations = new List<Specialization>
                {
                    new Specialization { Name = "Devastation", RoleId = damageRole.Id },
                    new Specialization { Name = "Preservation", RoleId = healerRole.Id },
                    new Specialization { Name = "Augmentation", RoleId = damageRole.Id },
                }
            },
            new WowClass
            {
                Name = "Hunter",
                Description = "A skilled ranged damage dealer with pets.",
                ImageUrl = "hunter.jpg",
                Specializations = new List<Specialization>
                {
                    new Specialization { Name = "Beast Mastery", RoleId = damageRole.Id },
                    new Specialization { Name = "Marksmanship", RoleId = damageRole.Id },
                    new Specialization { Name = "Survival", RoleId = damageRole.Id },
                }
            },
            new WowClass
            {
                Name = "Mage",
                Description = "A powerful ranged spellcaster.",
                ImageUrl = "mage.jpg",
                Specializations = new List<Specialization>
                {
                    new Specialization { Name = "Arcane", RoleId = damageRole.Id },
                    new Specialization { Name = "Fire", RoleId = damageRole.Id },
                    new Specialization { Name = "Frost", RoleId = damageRole.Id },
                }
            },
            new WowClass
            {
                Name = "Monk",
                Description = "A versatile martial artist.",
                ImageUrl = "monk.jpg",
                Specializations = new List<Specialization>
                {
                    new Specialization { Name = "Brewmaster", RoleId = tankRole.Id },
                    new Specialization { Name = "Mistweaver", RoleId = healerRole.Id },
                    new Specialization { Name = "Windwalker", RoleId = damageRole.Id },
                }
            },
            new WowClass
            {
                Name = "Paladin",
                Description = "A holy warrior with healing and tanking capabilities.",
                ImageUrl = "paladin.jpg",
                Specializations = new List<Specialization>
                {
                    new Specialization { Name = "Holy", RoleId = healerRole.Id },
                    new Specialization { Name = "Protection", RoleId = tankRole.Id },
                    new Specialization { Name = "Retribution", RoleId = damageRole.Id },
                }
            },
            new WowClass
            {
                Name = "Priest",
                Description = "A master of healing and light.",
                ImageUrl = "priest.jpg",
                Specializations = new List<Specialization>
                {
                    new Specialization { Name = "Discipline", RoleId = healerRole.Id },
                    new Specialization { Name = "Holy", RoleId = healerRole.Id },
                    new Specialization { Name = "Shadow", RoleId = damageRole.Id },
                }
            },
            new WowClass
            {
                Name = "Rogue",
                Description = "A stealthy melee damage dealer.",
                ImageUrl = "rogue.jpg",
                Specializations = new List<Specialization>
                {
                    new Specialization { Name = "Assassination", RoleId = damageRole.Id },
                    new Specialization { Name = "Outlaw", RoleId = damageRole.Id },
                    new Specialization { Name = "Subtlety", RoleId = damageRole.Id },
                }
            },
            new WowClass
            {
                Name = "Shaman",
                Description = "A versatile caster and melee fighter.",
                ImageUrl = "shaman.jpg",
                Specializations = new List<Specialization>
                {
                    new Specialization { Name = "Elemental", RoleId = damageRole.Id },
                    new Specialization { Name = "Enhancement", RoleId = damageRole.Id },
                    new Specialization { Name = "Restoration", RoleId = healerRole.Id },
                }
            },
            new WowClass
            {
                Name = "Warlock",
                Description = "A master of dark magic and demons.",
                ImageUrl = "warlock.jpg",
                Specializations = new List<Specialization>
                {
                    new Specialization { Name = "Affliction", RoleId = damageRole.Id },
                    new Specialization { Name = "Demonology", RoleId = damageRole.Id },
                    new Specialization { Name = "Destruction", RoleId = damageRole.Id },
                }
            },
            new WowClass
            {
                Name = "Warrior",
                Description = "A strong and resilient fighter.",
                ImageUrl = "warrior.jpg",
                Specializations = new List<Specialization>
                {
                    new Specialization { Name = "Arms", RoleId = damageRole.Id },
                    new Specialization { Name = "Fury", RoleId = damageRole.Id },
                    new Specialization { Name = "Protection", RoleId = tankRole.Id },
                }
            }
        };

        foreach (var wowClass in allClasses)
        {
            
            var existingClass = await context.WowClasses
                .Include(c => c.Specializations)
                .FirstOrDefaultAsync(c => c.Name == wowClass.Name);

            if (existingClass == null)
            {
                
                context.WowClasses.Add(wowClass);
            }
            else
            {
                
                foreach (var spec in wowClass.Specializations)
                {
                    bool specExists = existingClass.Specializations.Any(s => s.Name == spec.Name);
                    if (!specExists)
                    {
                        existingClass.Specializations.Add(new Specialization
                        {
                            Name = spec.Name,
                            RoleId = spec.RoleId
                        });
                    }
                }
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
            var role = new Roles { Name = roleName, ClassSpecializations = new List<Specialization>() };
            context.Roles.Add(role);
            await context.SaveChangesAsync();
            existingRoleNames.Add(roleName);
            return role;
        }
    }

    public static class TalentTreeSeeder
    {
       
            public static async Task SeedAsync(ApplicationDbContext context)
            {
                var specializations = await context.Specializations.ToListAsync();

                foreach (var spec in specializations)
                {
                var existingTalents = context.TalentTree.Where(t => t.SpecializationId == spec.Id);
                context.TalentTree.RemoveRange(existingTalents);
               
                        var talents = spec.Id switch
                        {
                            40 => new List<TalentNode>  // Blood
                    {
                        new TalentNode { Name = "Crimson Scourge", Description = "Your attacks cause enemies to bleed.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Bone Shield", Description = "Increases your damage absorption.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Blood Tap", Description = "Converts runes into runic power.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Sanguine Fortitude", Description = "Increases your health regeneration.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Death Pact", Description = "Sacrifice a portion of your health to heal.", SpecializationId = spec.Id, ParentId = null },
                    },
                            41 => new List<TalentNode>  // Frost (DK)
                    {
                        new TalentNode { Name = "Icy Touch", Description = "Your frost damage slows enemies.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Frozen Core", Description = "Increases critical strike chance with frost spells.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Chill of the Grave", Description = "Your attacks reduce enemy movement speed.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Glacial Advance", Description = "Your frost attacks have a chance to freeze targets.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Rime", Description = "Your frost spells generate additional runic power.", SpecializationId = spec.Id, ParentId = null },
                    },
                            42 => new List<TalentNode>  // Unholy
                    {
                        new TalentNode { Name = "Virulent Plague", Description = "Infect enemies with deadly diseases.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Death Coil", Description = "Unleashes dark energy to damage enemies.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Festering Strike", Description = "Inflicts wounds that explode.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Unholy Blight", Description = "Spreads diseases to nearby enemies.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Dark Transformation", Description = "Empowers your ghoul companion.", SpecializationId = spec.Id, ParentId = null },
                    },
                            43 => new List<TalentNode>  // Havoc (DH)
                    {
                        new TalentNode { Name = "Demonic Rush", Description = "Increases movement speed temporarily.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Chaos Strike", Description = "Strikes the enemy with chaotic energy.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Fel Barrage", Description = "Unleashes fel energy in a cone.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Nemesis", Description = "Increases critical strike chance.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Master of the Glaive", Description = "Increases glaive throwing damage.", SpecializationId = spec.Id, ParentId = null },
                    },
                            44 => new List<TalentNode>  // Vengeance (DH tank)
                    {
                        new TalentNode { Name = "Soul Barrier", Description = "Generates a shield absorbing damage.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Fiery Brand", Description = "Brands enemies to deal damage over time.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Immolation Aura", Description = "Damages nearby enemies with fire.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Sigil of Flame", Description = "Plants a sigil that explodes after a delay.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Demon Spikes", Description = "Reduces damage taken and increases mobility.", SpecializationId = spec.Id, ParentId = null },
                    },
                            45 => new List<TalentNode>  // Balance (Druid)
                    {
                        new TalentNode { Name = "Moonfire", Description = "Deals arcane damage over time.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Solar Wrath", Description = "Deals direct damage.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Stellar Flare", Description = "Deals damage over time and reduces healing.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Eclipse", Description = "Increases damage of solar and lunar spells.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Warrior of Elune", Description = "Increases attack speed and damage.", SpecializationId = spec.Id, ParentId = null },
                    },
                            46 => new List<TalentNode>  // Feral
                    {
                        new TalentNode { Name = "Rake", Description = "Deals bleed damage over time.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Shred", Description = "Deals high damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Rip", Description = "Deals bleed damage over time.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Berserk", Description = "Increases energy regeneration and damage.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Savage Roar", Description = "Increases damage done.", SpecializationId = spec.Id, ParentId = null },
                    },
                            47 => new List<TalentNode>  // Guardian
                    {
                        new TalentNode { Name = "Mangle", Description = "Deals damage and generates rage.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Thrash", Description = "Deals damage to all nearby enemies.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Ironfur", Description = "Increases armor.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Frenzied Regeneration", Description = "Heals you using rage.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Survival Instincts", Description = "Reduces damage taken.", SpecializationId = spec.Id, ParentId = null },
                    },
                            48 => new List<TalentNode>  // Restoration (Druid heal)
                    {
                        new TalentNode { Name = "Rejuvenation", Description = "Heals the target over time.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Lifebloom", Description = "Heals the target and blooms for extra healing.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Regrowth", Description = "Instant heal and heal over time.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Swiftmend", Description = "Instant heal using healing over time effects.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Wild Growth", Description = "Heals multiple allies over time.", SpecializationId = spec.Id, ParentId = null },
                    },
                            49 => new List<TalentNode>  // Devastation (Evoker)
                    {
                        new TalentNode { Name = "Essence Burst", Description = "Deals burst damage with essence.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Fire Breath", Description = "Breathes fire on enemies.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Dragon's Fury", Description = "Increases damage output.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Scales of the Dragon", Description = "Increases defense.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Wing Buffet", Description = "Knocks back enemies.", SpecializationId = spec.Id, ParentId = null },
                    },
                            50 => new List<TalentNode>  // Preservation (Evoker heal)
                    {
                        new TalentNode { Name = "Healing Breath", Description = "Heals allies in front of you.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Spirit Mend", Description = "Heals an ally over time.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Dragon's Grace", Description = "Increases healing power.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Protective Scales", Description = "Absorbs damage for allies.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Renewing Mist", Description = "Heals multiple allies.", SpecializationId = spec.Id, ParentId = null },
                    },
                            51 => new List<TalentNode> //Augumentation (Evoker)
                    {
                        new TalentNode { Name = "Augmented Breath", Description = "Increases the power of your breath attacks.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Empowered Scales", Description = "Increases the effectiveness of your protective scales.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Essence Infusion", Description = "Increases the healing of your essence abilities.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Temporal Anomaly", Description = "Creates a time anomaly that heals allies.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Echoing Rejuvenation", Description = "Heals allies over time.", SpecializationId = spec.Id, ParentId = null },

                    },
                            52 => new List<TalentNode>  // BeastMastery Hunter
                    {
                        new TalentNode { Name = "Bestial Wrath", Description = "Increases your pet's damage.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Kill Command", Description = "Commands your pet to attack.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Animal Companion", Description = "Summons an additional pet.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Aspect of the Wild", Description = "Increases your and your pet's damage.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Cobra Shot", Description = "Deals damage and generates focus.", SpecializationId = spec.Id, ParentId = null },
                    },
                            53 => new List<TalentNode>  // Marksmanship Hunter

                    {
                        new TalentNode { Name = "Aimed Shot", Description = "Deals high damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Rapid Fire", Description = "Fires a rapid series of shots.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Trick Shots", Description = "Your shots hit multiple targets.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Piercing Shot", Description = "Deals damage and pierces through enemies.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Chimaera Shot", Description = "Deals damage to two targets.", SpecializationId = spec.Id, ParentId = null },
                    },

                            54 => new List<TalentNode>  // Survival Hunter  

                    {
                        new TalentNode { Name = "Raptor Strike", Description = "Deals damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Mongoose Bite", Description = "Deals damage and increases your agility.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Flanking Strike", Description = "Deals damage and summons a pet.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Wildfire Infusion", Description = "Increases your damage.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Aspect of the Beast", Description = "Increases your pet's damage.", SpecializationId = spec.Id, ParentId = null },
                    },

                            55 => new List<TalentNode>  // Arcane (Mage)
                    {
                        new TalentNode { Name = "Arcane Barrage", Description = "Deals damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Arcane Missiles", Description = "Deals damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Arcane Orb", Description = "Deals damage to multiple targets.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Arcane Power", Description = "Increases your damage.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Presence of Mind", Description = "Increases your spell casting speed.", SpecializationId = spec.Id, ParentId = null },
                    },

                            56 => new List<TalentNode>  // Fire (Mage)
                    {
                        new TalentNode { Name = "Fireball", Description = "Deals damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Pyroblast", Description = "Deals high damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Combustion", Description = "Increases your damage.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Fire Blast", Description = "Deals damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Phoenix Flames", Description = "Deals damage to multiple targets.", SpecializationId = spec.Id, ParentId = null },
                    },

                            57 => new List<TalentNode>  // Frost (Mage)
                    {
                        new TalentNode { Name = "Frostbolt", Description = "Deals damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Ice Lance", Description = "Deals damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Frozen Orb", Description = "Deals damage to multiple targets.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Ice Barrier", Description = "Absorbs damage.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Blizzard", Description = "Deals damage to multiple targets.", SpecializationId = spec.Id, ParentId = null },
                    },

                            58 => new List<TalentNode>  // Brewmaster (Monk)
                    {
                        new TalentNode { Name = "Tiger Palm", Description = "Deals damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Blackout Kick", Description = "Deals damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Keg Smash", Description = "Deals damage to multiple targets.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Breath of Fire", Description = "Deals damage to multiple targets.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Celestial Brew", Description = "Absorbs damage.", SpecializationId = spec.Id, ParentId = null },
                    },

                            59 => new List<TalentNode>  // Mistweaver (Monk)
                    {
                        new TalentNode { Name = "Soothing Mist", Description = "Heals allies over time.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Vivify", Description = "Heals a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Essence Font", Description = "Heals multiple allies.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Renewing Mist", Description = "Heals multiple allies over time.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Life Cocoon", Description = "Absorbs damage for an ally.", SpecializationId = spec.Id, ParentId = null },
                    },

                            60 => new List<TalentNode>  // Windwalker (Monk)
                    {
                        new TalentNode { Name = "Tiger Palm", Description = "Deals damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Blackout Kick", Description = "Deals damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Rising Sun Kick", Description = "Deals damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Fists of Fury", Description = "Deals damage to multiple targets.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Storm, Earth, and Fire", Description = "Summons spirits to attack enemies.", SpecializationId = spec.Id, ParentId = null },
                    },

                            61 => new List<TalentNode>  // Holy (Paladin)
                    {
                        new TalentNode { Name = "Holy Shock", Description = "Heals an ally or damages an enemy.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Light of Dawn", Description = "Heals multiple allies.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Holy Prism", Description = "Deals damage and heals allies.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Beacon of Light", Description = "Heals the target over time.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Avenging Wrath", Description = "Increases healing done.", SpecializationId = spec.Id, ParentId = null },
                    },


                            62 => new List<TalentNode>  // Protection (Paladin)
                    {
                        new TalentNode { Name = "Shield of the Righteous", Description = "Absorbs damage and increases block chance.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Avenger's Shield", Description = "Throws a shield that stuns and damages enemies.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Consecration", Description = "Deals damage to enemies in an area.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Divine Shield", Description = "Absorbs damage and prevents death.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Guardian of Ancient Kings", Description = "Increases damage absorption.", SpecializationId = spec.Id, ParentId = null },
                    },

                            63 => new List<TalentNode>  // Retribution (Paladin)
                    {
                        new TalentNode { Name = "Templar's Verdict", Description = "Deals damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Blade of Justice", Description = "Deals damage and generates holy power.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Divine Storm", Description = "Deals damage to multiple targets.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Crusade", Description = "Increases damage and healing.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Avenging Wrath", Description = "Increases damage done.", SpecializationId = spec.Id, ParentId = null },
                    },
                     

                            64 => new List<TalentNode> //Discipline Priest
                    {
                        new TalentNode { Name = "Penance", Description = "Heals an ally or damages an enemy.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Shadow Mend", Description = "Heals an ally at the cost of your own health.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Power Word: Shield", Description = "Absorbs damage for an ally.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Atonement", Description = "Heals allies when you deal damage.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Rapture", Description = "Increases the effectiveness of your shields.", SpecializationId = spec.Id, ParentId = null },
                    },

                            65 => new List<TalentNode> //Holy Priest
                    {
                        new TalentNode { Name = "Holy Word: Serenity", Description = "Heals a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Circle of Healing", Description = "Heals multiple allies.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Guardian Spirit", Description = "Absorbs damage for an ally.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Divine Hymn", Description = "Heals multiple allies over time.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Holy Nova", Description = "Deals damage and heals allies in an area.", SpecializationId = spec.Id, ParentId = null },
                    },

                            66 => new List<TalentNode> //Shadow Priest
                    {
                        new TalentNode { Name = "Mind Blast", Description = "Deals damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Shadow Word: Pain", Description = "Deals damage over time.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Vampiric Touch", Description = "Deals damage and heals you for a portion of the damage dealt.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Mind Flay", Description = "Deals damage over time and slows the target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Voidform", Description = "Increases your damage and healing.", SpecializationId = spec.Id, ParentId = null },
                    },
                        
                            67 => new List<TalentNode> //Assassination Rogue
                    {
                        new TalentNode { Name = "Garrote", Description = "Deals damage over time and silences the target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Rupture", Description = "Deals damage over time.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Envenom", Description = "Deals damage and consumes combo points.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Vendetta", Description = "Increases damage dealt to the target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Crimson Tempest", Description = "Deals damage to all nearby enemies.", SpecializationId = spec.Id, ParentId = null },
                    },

                            68 => new List<TalentNode> //Outlaw Rogue
                    {
                        new TalentNode { Name = "Sinister Strike", Description = "Deals damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Pistol Shot", Description = "Deals damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Blade Flurry", Description = "Deals damage to multiple targets.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Adrenaline Rush", Description = "Increases energy regeneration.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Between the Eyes", Description = "Stuns the target and deals damage.", SpecializationId = spec.Id, ParentId = null },
                    },

                            69 => new List<TalentNode> //Subtlety Rogue
                    {
                        new TalentNode { Name = "Backstab", Description = "Deals damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Shadow Dance", Description = "Allows you to use stealth abilities.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Shadowstrike", Description = "Deals damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Symbols of Death", Description = "Increases damage dealt.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Eviscerate", Description = "Deals damage and consumes combo points.", SpecializationId = spec.Id, ParentId = null },
                    },

                            70 => new List<TalentNode> //Elemental Shaman
                    {
                        new TalentNode { Name = "Lava Burst", Description = "Deals damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Chain Lightning", Description = "Deals damage to multiple targets.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Earthquake", Description = "Deals damage to all nearby enemies.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Stormkeeper", Description = "Increases damage dealt.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Ascendance", Description = "Increases damage and healing.", SpecializationId = spec.Id, ParentId = null },
                    },

                            71 => new List<TalentNode> //Enhancement Shaman
                    {
                        new TalentNode { Name = "Stormstrike", Description = "Deals damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Flame Shock", Description = "Deals damage over time.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Feral Spirit", Description = "Summons spirit wolves to fight for you.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Ascendance", Description = "Increases damage and healing.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Windfury Totem", Description = "Increases attack speed.", SpecializationId = spec.Id, ParentId = null },
                    },

                            72 => new List<TalentNode> //Restoration Shaman
                    {
                        new TalentNode { Name = "Healing Surge", Description = "Heals a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Chain Heal", Description = "Heals multiple allies.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Riptide", Description = "Heals an ally over time.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Healing Tide Totem", Description = "Heals multiple allies over time.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Ascendance", Description = "Increases healing done.", SpecializationId = spec.Id, ParentId = null },
                    },

                            73 => new List<TalentNode> //Affliction Warlock
                    {
                        new TalentNode { Name = "Corruption", Description = "Deals damage over time.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Unstable Affliction", Description = "Deals damage over time and silences the target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Haunt", Description = "Deals damage and heals you for a portion of the damage dealt.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Shadow Bolt", Description = "Deals damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Malediction", Description = "Increases damage dealt.", SpecializationId = spec.Id, ParentId = null },
                    },

                            74 => new List<TalentNode> //Demonology Warlock
                    {
                        new TalentNode { Name = "Demonbolt", Description = "Deals damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Summon Felguard", Description = "Summons a felguard to fight for you.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Demonic Empowerment", Description = "Increases the power of your demons.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Hand of Gul'dan", Description = "Deals damage and summons demons.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Demonic Tyrant", Description = "Increases the power of your demons.", SpecializationId = spec.Id, ParentId = null },
                    },

                            75 => new List<TalentNode> //Destruction Warlock
                    {
                        new TalentNode { Name = "Incinerate", Description = "Deals damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Chaos Bolt", Description = "Deals high damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Conflagrate", Description = "Deals damage and consumes embers.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Shadowburn", Description = "Deals damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Dark Soul", Description = "Increases damage and healing.", SpecializationId = spec.Id, ParentId = null },
                    },

                            76 => new List<TalentNode> //Arms Warrior
                    {
                        new TalentNode { Name = "Mortal Strike", Description = "Deals damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Overpower", Description = "Deals damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Execute", Description = "Deals damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Rend", Description = "Deals damage over time.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Bladestorm", Description = "Deals damage to all nearby enemies.", SpecializationId = spec.Id, ParentId = null },
                    },

                            77 => new List<TalentNode> //Fury Warrior
                    {
                        new TalentNode { Name = "Bloodthirst", Description = "Deals damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Raging Blow", Description = "Deals damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Rampage", Description = "Deals damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Execute", Description = "Deals damage to a single target.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Recklessness", Description = "Increases damage and healing.", SpecializationId = spec.Id, ParentId = null },
                    },

                            78 => new List<TalentNode> //Protection Warrior
                    {
                        new TalentNode { Name = "Shield Slam", Description = "Deals damage and generates rage.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Thunder Clap", Description = "Deals damage to all nearby enemies.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Shield Block", Description = "Increases block chance.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Last Stand", Description = "Absorbs damage and increases health.", SpecializationId = spec.Id, ParentId = null },
                        new TalentNode { Name = "Rallying Cry", Description = "Increases health of all allies.", SpecializationId = spec.Id, ParentId = null },
                    }



                        };
                        
                        if(talents != null)
                        {
                            await context.TalentTree.AddRangeAsync(talents);
                        }
                   
                    }

                await context.SaveChangesAsync();
        }

               
            }
    }




using MythicTeamOptimiser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MythicTeamOptimiser.Algorithms.ValidTeamCompsFinder
{
    public class LookupSpec
    {
        //Singleton
        private static LookupSpec instance;
        public static LookupSpec Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new LookupSpec();
                }
                return instance;
            }
        }

        private List<SpecRole> specRoles;

        private LookupSpec()
        {
            specRoles = new List<SpecRole>();
            specRoles.Add(new SpecRole("Death Knight", "Blood", CharacterRole.Tank));
            specRoles.Add(new SpecRole("Death Knight", "Frost", CharacterRole.Dps));
            specRoles.Add(new SpecRole("Death Knight", "Unholy", CharacterRole.Dps));

            specRoles.Add(new SpecRole("Demon Hunter", "Havoc", CharacterRole.Dps));
            specRoles.Add(new SpecRole("Demon Hunter", "Vengeance", CharacterRole.Tank));

            specRoles.Add(new SpecRole("Druid", "Balance", CharacterRole.Dps));
            specRoles.Add(new SpecRole("Druid", "Feral", CharacterRole.Dps));
            specRoles.Add(new SpecRole("Druid", "Guardian", CharacterRole.Tank));
            specRoles.Add(new SpecRole("Druid", "Restoration", CharacterRole.Healer));

            specRoles.Add(new SpecRole("Hunter", "Survival", CharacterRole.Dps));
            specRoles.Add(new SpecRole("Hunter", "Marksmanship", CharacterRole.Dps));
            specRoles.Add(new SpecRole("Hunter", "Beast Mastery", CharacterRole.Dps));

            specRoles.Add(new SpecRole("Mage", "Arcane", CharacterRole.Dps));
            specRoles.Add(new SpecRole("Mage", "Fire", CharacterRole.Dps));
            specRoles.Add(new SpecRole("Mage", "Frost", CharacterRole.Dps));

            specRoles.Add(new SpecRole("Monk", "Brewmaster", CharacterRole.Tank));
            specRoles.Add(new SpecRole("Monk", "Mistweaver", CharacterRole.Healer));
            specRoles.Add(new SpecRole("Monk", "Windwalker", CharacterRole.Dps));

            specRoles.Add(new SpecRole("Paladin", "Holy", CharacterRole.Healer));
            specRoles.Add(new SpecRole("Paladin", "Protection", CharacterRole.Tank));
            specRoles.Add(new SpecRole("Paladin", "Retribution", CharacterRole.Dps));

            specRoles.Add(new SpecRole("Priest", "Holy", CharacterRole.Healer));
            specRoles.Add(new SpecRole("Priest", "Discipline", CharacterRole.Healer));
            specRoles.Add(new SpecRole("Priest", "Shadow", CharacterRole.Dps));

            specRoles.Add(new SpecRole("Rogue", "Assassination", CharacterRole.Dps));
            specRoles.Add(new SpecRole("Rogue", "Outlaw", CharacterRole.Dps));
            specRoles.Add(new SpecRole("Rogue", "Subtlety", CharacterRole.Dps));

            specRoles.Add(new SpecRole("Shaman", "Elemental", CharacterRole.Dps));
            specRoles.Add(new SpecRole("Shaman", "Enhancement", CharacterRole.Dps));
            specRoles.Add(new SpecRole("Shaman", "Restoration", CharacterRole.Healer));

            specRoles.Add(new SpecRole("Warlock", "Affliction", CharacterRole.Dps));
            specRoles.Add(new SpecRole("Warlock", "Demonology", CharacterRole.Dps));
            specRoles.Add(new SpecRole("Warlock", "Destruction", CharacterRole.Dps));

            specRoles.Add(new SpecRole("Warrior", "Arms", CharacterRole.Dps));
            specRoles.Add(new SpecRole("Warrior", "Fury", CharacterRole.Dps));
            specRoles.Add(new SpecRole("Warrior", "Protection", CharacterRole.Tank));

            specRoles = specRoles.OrderBy(x => x.Role).ToList();
        }

        public SpecRole GetSpecRoleFromInt(int specRoleIndex)
        {
            return specRoles[specRoleIndex];
        }

        public int GetIntFromPlayerCharacterModel(PlayerCharacterModel specRole)
        {
            for(int i = 0; i < specRoles.Count; i++)
            {
                if(specRoles[i].CharClass == specRole.ClassName)
                {
                    if (specRoles[i].Spec == specRole.SpecName)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public CharacterRole GetRoleFromPlayerCharacterModel(PlayerCharacterModel pcm)
        {
            return GetSpecRoleFromInt(GetIntFromPlayerCharacterModel(pcm)).Role;
        }

        public SpecRole GetSpecRoleFromPlayerCharacterModel(PlayerCharacterModel pcm)
        {
            return GetSpecRoleFromInt(GetIntFromPlayerCharacterModel(pcm));
        }

        public PlayerCharacterModel GetPlayerCharacterModelFromInt(int specRoleIndex)
        {
            SpecRole sr = GetSpecRoleFromInt(specRoleIndex);
            return new PlayerCharacterModel { ClassName = sr.CharClass, SpecName = sr.Spec };
        }

        public int GetSpecsCount()
        {
            return specRoles.Count;
        }
    }
}

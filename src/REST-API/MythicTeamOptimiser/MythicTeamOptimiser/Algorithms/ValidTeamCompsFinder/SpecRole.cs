using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public enum CharacterRole
{
    Tank,
    Healer,
    Dps
}

namespace MythicTeamOptimiser.Algorithms.ValidTeamCompsFinder
{
    public class SpecRole
    {
        public string CharClass { get; private set; }
        public string Spec { get; private set; }
        public CharacterRole Role { get; private set; }

        public SpecRole(string charClass, string spec, CharacterRole role)
        {
            this.CharClass = charClass;
            this.Spec = spec;
            this.Role = role;
        }

        public override string ToString()
        {
            return $"{Spec} {CharClass}";
        }
    }
}

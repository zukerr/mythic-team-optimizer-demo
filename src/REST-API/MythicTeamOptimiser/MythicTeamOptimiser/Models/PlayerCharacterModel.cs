using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MythicTeamOptimiser.Models
{
    public class PlayerCharacterModel
    {
        public string ClassName { get; set; }
        public string SpecName { get; set; }

        public override string ToString()
        {
            return $"{nameof(ClassName)}: {ClassName}, {nameof(SpecName)}: {SpecName}";
        }

        public override bool Equals(object obj)
        {
            if(obj is PlayerCharacterModel)
            {
                PlayerCharacterModel other = (PlayerCharacterModel)obj;
                return ClassName.Equals(other.ClassName) && SpecName.Equals(other.SpecName);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ClassName, SpecName);
        }
    }
}

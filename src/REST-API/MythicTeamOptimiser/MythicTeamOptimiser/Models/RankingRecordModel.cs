using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace MythicTeamOptimiser.Models
{
    public class RankingRecordModel
    {
        public int? Rank { get; set; }
        public double? Score { get; set; }
        public string DungeonName { get; set; }
        public int? KeystoneLevel { get; set; }
        public List<PlayerCharacterModel> TeamMembers { get; set; }

        public override string ToString()
        {
            string result = $"{nameof(Rank)}: {Rank}, {nameof(Score)}: {Score}, {nameof(DungeonName)}: {DungeonName}, {nameof(KeystoneLevel)}: {KeystoneLevel}";
            foreach(PlayerCharacterModel pcm in TeamMembers)
            {
                result += $", {pcm}";
            }
            return result;
        }

        public bool ContainsSpecInTeam(string spec)
        {
            foreach(PlayerCharacterModel player in TeamMembers)
            {
                if(player.SpecName.Equals(spec))
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasSameTeamMembersAs(RankingRecordModel other)
        {
            foreach(PlayerCharacterModel elem in TeamMembers)
            {
                if(!other.ContainsSpecInTeam(elem.SpecName))
                {
                    return false;
                }
            }
            return true;
        }

        /*
        public string ToJsonString()
        {
            string result = $"{{\"{nameof(DungeonName)}\":\"{DungeonName}\",\"roster\":[";
            for(int i = 0; i < TeamMembers.Count; i++)
            {
                result += $"{{{TeamMembers[i].ToJsonString()}}}";
                if(i < TeamMembers.Count - 1)
                {
                    result += ",";
                }
            }
            result += "}";
            return result;
        }
        */
    }
}

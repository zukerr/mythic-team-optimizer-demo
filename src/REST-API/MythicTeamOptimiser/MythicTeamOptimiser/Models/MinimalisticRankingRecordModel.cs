using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MythicTeamOptimiser.Models
{
    public class MinimalisticRankingRecordModel
    {
        public string DungeonName { get; set; }
        public List<PlayerCharacterModel> TeamMembers { get; set; }

        public void CreateFromRankingRecordModel(RankingRecordModel input)
        {
            DungeonName = input.DungeonName;
            TeamMembers = input.TeamMembers;
        }
    }
}

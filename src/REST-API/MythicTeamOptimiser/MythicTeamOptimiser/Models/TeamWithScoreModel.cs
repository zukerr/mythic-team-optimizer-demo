using MythicTeamOptimiser.Algorithms.ValidTeamCompsFinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MythicTeamOptimiser.Models
{
    public class TeamWithScoreModel
    {
        public List<PlayerCharacterModel> TeamMembers { get; set; }
        public float Score { get; set; }

        public void CreateFromTeamWithScore(TeamWithScore input)
        {
            TeamMembers = input.TeamComp;
            Score = input.TeamScore;
        }
    }
}

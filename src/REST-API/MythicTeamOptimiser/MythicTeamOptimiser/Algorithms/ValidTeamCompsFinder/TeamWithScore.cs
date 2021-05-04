using MythicTeamOptimiser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MythicTeamOptimiser.Algorithms.ValidTeamCompsFinder
{
    public class TeamWithScore
    {
        private List<PlayerCharacterModel> teamComp;
        private float teamScore;
        public float TeamScore => teamScore;
        public List<PlayerCharacterModel> TeamComp => teamComp;

        public TeamWithScore(List<int> rawTeamComp, float teamScore)
        {
            this.teamScore = teamScore;
            teamComp = new List<PlayerCharacterModel>();
            foreach(int elem in rawTeamComp)
            {
                SpecRole specRoleFromInt = LookupSpec.Instance.GetSpecRoleFromInt(elem);
                teamComp.Add(new PlayerCharacterModel { ClassName = specRoleFromInt.CharClass, SpecName = specRoleFromInt.Spec });
            }
        }

        public override string ToString()
        {
            string result = $"Score: {teamScore}";
            foreach (PlayerCharacterModel pcm in teamComp)
            {
                result += $", {pcm}";
            }
            return result;
        }
    }
}

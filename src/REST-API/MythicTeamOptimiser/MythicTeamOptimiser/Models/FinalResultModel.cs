using MythicTeamOptimiser.Algorithms.ValidTeamCompsFinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MythicTeamOptimiser.Models
{
    public class FinalResultModel
    {
        public MinimalisticRankingRecordModel RaiderioAnalysysResult { get; set; }
        public List<TeamWithScoreModel> RaidbotsAnalysysResult { get; set; }

        public void CreateFromCalculatedData(RankingRecordModel rawRaiderioResults, List<TeamWithScore> rawRaidbotsResults)
        {
            RaiderioAnalysysResult = new MinimalisticRankingRecordModel();
            RaiderioAnalysysResult.CreateFromRankingRecordModel(rawRaiderioResults);
            RaidbotsAnalysysResult = new List<TeamWithScoreModel>();
            foreach(TeamWithScore elem in rawRaidbotsResults)
            {
                TeamWithScoreModel model = new TeamWithScoreModel();
                model.CreateFromTeamWithScore(elem);
                RaidbotsAnalysysResult.Add(model);
            }
        }
    }
}

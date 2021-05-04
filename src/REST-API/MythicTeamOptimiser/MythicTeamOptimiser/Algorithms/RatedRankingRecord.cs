using MythicTeamOptimiser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MythicTeamOptimiser.Algorithms
{
    public class RatedRankingRecord
    {
        private RankingRecordModel rankingRecordModel;
        public RankingRecordModel InnerRankingRecordModel
        {
            get
            {
                return rankingRecordModel;
            }
        }

        private float scoreRating = 0;
        private float repetitivenessRating = 0;
        public float RepetitivenessRating
        {
            get
            {
                return repetitivenessRating;
            }
            set
            {
                repetitivenessRating = value;
            }
        }

        private float scoreWeight = 1f;
        private float repetitivenessWeight = 1f;

        private float totalRating = 0;
        public float TotalRating
        {
            get
            {
                totalRating = 0;
                totalRating += scoreRating * scoreWeight;
                totalRating += repetitivenessRating * repetitivenessWeight;
                return totalRating;
            }
        }

        public RatedRankingRecord(RankingRecordModel rankingRecordModel)
        {
            this.rankingRecordModel = rankingRecordModel;
            scoreRating = (float)this.rankingRecordModel.Score;
        }
    }
}

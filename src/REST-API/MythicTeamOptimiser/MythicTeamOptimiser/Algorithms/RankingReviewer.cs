using MythicTeamOptimiser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MythicTeamOptimiser.Algorithms
{
    public class RankingReviewer
    {
        private List<RankingRecordModel> inputRankingList;

        public RankingReviewer(List<RankingRecordModel> inputRanking)
        {
            inputRankingList = inputRanking;
        }

        public List<RankingRecordModel> GetRatedRanking()
        {
            //convert vanilla input list to rated list
            List<RatedRankingRecord> ratedList = new List<RatedRankingRecord>();
            foreach(RankingRecordModel elem in inputRankingList)
            {
                ratedList.Add(new RatedRankingRecord(elem));
            }

            //Now we have populated rated list with data. We have to set up score values that wasn't filled by the constructor
            foreach (RatedRankingRecord elem in ratedList)
            {
                foreach (RatedRankingRecord innerElem in ratedList)
                {
                    if(elem.InnerRankingRecordModel.HasSameTeamMembersAs(innerElem.InnerRankingRecordModel))
                    {
                        elem.RepetitivenessRating += 1;
                    }
                }
            }

            //Now we have rated list fully scored. Next step is to sort values in the rated list by total rating calculated on the fly.
            ratedList = ratedList.OrderByDescending(x => x.TotalRating).ToList();

            List<RankingRecordModel> result = new List<RankingRecordModel>();
            foreach (RatedRankingRecord elem in ratedList)
            {
                result.Add(elem.InnerRankingRecordModel);
            }
            return result;
        }

        public RankingRecordModel GetTopRatedModel()
        {
            return GetRatedRanking()[0];
        }
    }
}

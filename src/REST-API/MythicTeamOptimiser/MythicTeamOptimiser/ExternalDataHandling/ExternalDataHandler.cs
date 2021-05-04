using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using MythicTeamOptimiser.Models;
using Newtonsoft.Json;

namespace MythicTeamOptimiser.ExternalDataHandling
{
    public class ExternalDataHandler
    {
        private const double MIN_SCORE = 20;
        private const int RANKING_PAGE_CAPACITY = 20;
        private const int MAX_SERVICABLE_PAGES = 5;

        private async Task<List<RankingRecordModel>> GetRequestRaiderIoRankingPage(string regionName, string dungeonName, int pageNumber)
        {
            using (HttpClient client = new HttpClient())
            {
                dungeonName = dungeonName.ToLower().Replace(' ', '-');
                string url = $"https://raider.io/api/v1/mythic-plus/runs?season=season-sl-1&region={regionName}&dungeon={dungeonName}&affixes=current&page={pageNumber}";
                var myContent = await client.GetStringAsync(url);
                dynamic rankingRecords = JsonConvert.DeserializeObject(myContent);
                List<RankingRecordModel> parsedRecords = new List<RankingRecordModel>();
                foreach(var elem in rankingRecords.rankings)
                {
                    //Console.WriteLine(elem.ToString());
                    parsedRecords.Add
                        (
                            new RankingRecordModel
                            { 
                                Rank = elem.rank,
                                Score = elem.score,
                                DungeonName = elem.run.dungeon.name,
                                KeystoneLevel = elem.run.mythic_level,
                                TeamMembers = new List<PlayerCharacterModel>()
                            }
                        );

                    
                    for(int i = 0; i < 5; i++)
                    {
                        PlayerCharacterModel teamMember = new PlayerCharacterModel 
                        { 
                            ClassName = elem.run.roster[i].character.@class.name, 
                            SpecName = elem.run.roster[i].character.spec.name 
                        };
                        parsedRecords[parsedRecords.Count - 1].TeamMembers.Add(teamMember);
                    }
                    
                    //Console.WriteLine(parsedRecords[parsedRecords.Count - 1]);
                }
                return parsedRecords;
            }
        }

        public async Task<List<RankingRecordModel>> GetParsedRaiderIoRanking(string dungeonName)
        {
            List<RankingRecordModel> fullRanking = new List<RankingRecordModel>();

            double currentMinScore = 100;
            List<RankingRecordModel> currentPage;

            List<string> regions = new List<string> { "us", "eu", "kr", "tw" };

            foreach(string region in regions)
            {
                for (int i = 0; i < MAX_SERVICABLE_PAGES; i++)
                {
                    currentPage = await GetRequestRaiderIoRankingPage(region, dungeonName, i);
                    fullRanking.AddRange(currentPage);

                    currentMinScore = (double)currentPage[currentPage.Count - 1].Score;
                }
            }

            return fullRanking;
        }
    }
}

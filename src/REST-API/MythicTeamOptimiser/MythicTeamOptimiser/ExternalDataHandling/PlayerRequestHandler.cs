using MythicTeamOptimiser.Algorithms;
using MythicTeamOptimiser.Algorithms.CspProblemSolver;
using MythicTeamOptimiser.Algorithms.ValidTeamCompsFinder;
using MythicTeamOptimiser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace MythicTeamOptimiser.ExternalDataHandling
{
    public class PlayerRequestHandler
    {
        private string dungeonName;
        private PlayerCharacterModel playerCharacter;

        public PlayerRequestHandler(string dungeonName, string playerClass, string playerSpec)
        {
            this.dungeonName = dungeonName;
            playerCharacter = new PlayerCharacterModel { ClassName = playerClass, SpecName = playerSpec };
        }

        public async Task<FinalResultModel> GetBestTeamComp()
        {
            //start csp to find all possible team comps
            Task<List<List<int>>> csp = GetAllPossibleTeamCompsRaw();
            Console.WriteLine("Launched csp.");

            StaticDataDownloader sdd = new StaticDataDownloader();
            Task<Dictionary<PlayerCharacterModel, float>> avgDpsSims = sdd.GetAvgDpsFromRaidbotsStaticFiles();
            Console.WriteLine("Launched raidbots data download and parse.");

            ExternalDataHandler edh = new ExternalDataHandler();

            Task<List<RankingRecordModel>> rankingRetriever = edh.GetParsedRaiderIoRanking(dungeonName);
            Console.WriteLine("Launched call to external api for data extraction.");

            List<RankingRecordModel> retrievedRanking = await rankingRetriever;
            Console.WriteLine("Received data from external api.");

            Dictionary<PlayerCharacterModel, float> avgDpsSimsData = await avgDpsSims;
            Console.WriteLine("Received and parsed data from raidbots: ");
            foreach (KeyValuePair<PlayerCharacterModel, float> elem in avgDpsSimsData)
            {
                Console.WriteLine($"{elem.Key} : {elem.Value}");
            }

            //filter ranking by spec
            retrievedRanking = retrievedRanking.Where(x => x.ContainsSpecInTeam(playerCharacter.SpecName)).ToList();

            //wait for csp results
            List<List<int>> cspResults = await csp;
            Console.WriteLine("Csp finished.");

            List<TeamWithScore> scoredTeams = ConvertRawCspToScoredTeams(cspResults, avgDpsSimsData);
            scoredTeams = scoredTeams.OrderByDescending(x => x.TeamScore).ToList();

            System.Console.WriteLine($"Elements containing given class and spec: {retrievedRanking.Count}");
            if(retrievedRanking.Count == 0)
            {
                //return "not enough data for given input.";
            }

            RankingReviewer reviewer = new RankingReviewer(retrievedRanking);
            scoredTeams = scoredTeams.GetRange(0, 10);
            FinalResultModel finalResults = new FinalResultModel();
            finalResults.CreateFromCalculatedData(reviewer.GetTopRatedModel(), scoredTeams);
            return finalResults;
        }

        private async Task<string> GetAllPossibleTeamComps()
        {
            //test all available team comps
            CspProblemSpecRoles cspProblemSpecRoles = new CspProblemSpecRoles(playerCharacter);
            CspBacktracking cspBacktracking = new CspBacktracking(cspProblemSpecRoles);
            await Task.Run(cspBacktracking.BacktrackFromRoot);
            return cspBacktracking.GetReadableResults();
        }

        private async Task<List<List<int>>> GetAllPossibleTeamCompsRaw()
        {
            CspProblemSpecRoles cspProblemSpecRoles = new CspProblemSpecRoles(playerCharacter);
            CspBacktracking cspBacktracking = new CspBacktracking(cspProblemSpecRoles);
            await Task.Run(cspBacktracking.BacktrackFromRoot);
            return cspBacktracking.GetRawResults();
        }

        private List<TeamWithScore> ConvertRawCspToScoredTeams(List<List<int>> rawCspResults, Dictionary<PlayerCharacterModel, float> rawRaidbotsDpsData)
        {
            List<TeamWithScore> scoredTeams = new List<TeamWithScore>();
            foreach (List<int> elem in rawCspResults)
            {
                float summaryScore = 0;
                foreach (int playerChar in elem)
                {
                    PlayerCharacterModel pcm = LookupSpec.Instance.GetPlayerCharacterModelFromInt(playerChar);
                    if(rawRaidbotsDpsData.ContainsKey(pcm))
                    {
                        summaryScore += rawRaidbotsDpsData[pcm];
                    }
                }
                scoredTeams.Add(new TeamWithScore(elem, summaryScore));
            }
            return scoredTeams;
        }
    }
}

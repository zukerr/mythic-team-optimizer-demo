using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Windows;
using System.IO;
using System.ComponentModel;
using MythicTeamOptimiser.Algorithms.ValidTeamCompsFinder;
using MythicTeamOptimiser.Models;
using System.Globalization;

namespace MythicTeamOptimiser.ExternalDataHandling
{
    public class StaticDataDownloader
    {
        private string fileURL = "https://www.raidbots.com/static/analysis/top/summary.csv";
        private string fileName = "summary.csv";
        private string rootPath;
        private string filePath;

        private Dictionary<PlayerCharacterModel, float> dataDict;
        private Dictionary<PlayerCharacterModel, int> counterDict;

        public StaticDataDownloader()
        {
            rootPath = Directory.GetCurrentDirectory();
            filePath = Path.Combine(rootPath, fileName);
        }

        public async Task<Dictionary<PlayerCharacterModel, float>> GetAvgDpsFromRaidbotsStaticFiles()
        {
            await DownloadFreshData();
            return dataDict;
        }

        private async Task DownloadFreshData()
        {
            try
            {
                if(File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                WebClient webClient = new WebClient();
                //webClient.DownloadFileCompleted += new AsyncCompletedEventHandler();
                await Task.Run(() => webClient.DownloadFile(new Uri(fileURL), fileName));

                //we have downloaded data - parse it and calc avg dps
                ParseDataFromDownloadedFile();
                CalculateAvgDps();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading data from raidbots.com: {ex}");
            }
        }

        private void ParseDataFromDownloadedFile()
        {
            using (StreamReader dataReader = new StreamReader(filePath))
            {
                dataDict = new Dictionary<PlayerCharacterModel, float>();
                counterDict = new Dictionary<PlayerCharacterModel, int>();
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

                dataReader.ReadLine();
                while (!dataReader.EndOfStream)
                {
                    string line = dataReader.ReadLine();
                    string[] values = line.Split(',');

                    PlayerCharacterModel characterInLine = new PlayerCharacterModel { ClassName = textInfo.ToTitleCase(values[3]), SpecName = textInfo.ToTitleCase(values[4]) };
                    if (dataDict.ContainsKey(characterInLine))
                    {
                        dataDict[characterInLine] += float.Parse(values[8]);
                        counterDict[characterInLine]++;
                    }
                    else
                    {
                        dataDict.Add(characterInLine, float.Parse(values[8]));
                        counterDict.Add(characterInLine, 1);
                    }
                }
            }
        }

        private void CalculateAvgDps()
        {
            foreach(PlayerCharacterModel elem in dataDict.Keys)
            {
                dataDict[elem] /= counterDict[elem];
            }
        }
    }
}

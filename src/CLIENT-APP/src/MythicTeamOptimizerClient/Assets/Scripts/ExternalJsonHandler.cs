using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Net.Http;
using System.Dynamic;

class ExternalJsonHandler
{
    public static void HandleRetrievedData(ResultsController uiResultsController, SpecDataControl strToIconConverter, string data)
    {
        Debug.Log("Started handling retrieved data.");
        Debug.Log(data);

        dynamic externalData = JsonConvert.DeserializeObject(data);

        Debug.Log("After dynamic.");
        Debug.Log(externalData);

        //handle raiderio data
        List<Sprite> raiderioSprites = new List<Sprite>();
        foreach(var elem in externalData.raiderioAnalysysResult.teamMembers)
        {
            Debug.Log($"elem: {elem.className}, {elem.specName}");
            raiderioSprites.Add(strToIconConverter.GetImageOfClassSpec((string)elem.className, (string)elem.specName));
            Debug.Log($"Added sprite.");
        }
        uiResultsController.AddRaiderioIcons(raiderioSprites);
        uiResultsController.SetDungeonText($"({(string)externalData.raiderioAnalysysResult.dungeonName})");

        Debug.Log("After handled raiderio data.");

        //handle raidbots data
        foreach (var rankingElem in externalData.raidbotsAnalysysResult)
        {
            List<Sprite> raidbotsSprites = new List<Sprite>();
            foreach (var elem in rankingElem.teamMembers)
            {
                raidbotsSprites.Add(strToIconConverter.GetImageOfClassSpec((string)elem.className, (string)elem.specName));
            }
            uiResultsController.AddRaidbotsElement(raidbotsSprites);
        }

        Debug.Log("Finished handling retrieved data.");
    }
}

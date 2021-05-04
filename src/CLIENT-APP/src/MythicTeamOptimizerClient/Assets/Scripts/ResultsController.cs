using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultsController : MonoBehaviour
{
    [SerializeField]
    private GameObject raiderioContent = null;
    [SerializeField]
    private GameObject raidbotsContent = null;
    [SerializeField]
    private GameObject raiderioElementPrefab = null;
    [SerializeField]
    private GameObject raidbotsElementPrefab = null;
    [SerializeField]
    private TextMeshProUGUI dungeonText = null;

    public void AddRaiderioIcons(List<Sprite> elements)
    {
        if(elements.Count != 5)
        {
            Debug.LogError($"ResultsController:{nameof(AddRaiderioIcons)}(List<Sprite> elements) : ERROR: Not enough elements.");
            return;
        }

        for(int i = 0; i < elements.Count; i++)
        {
            GameObject createdElement = Instantiate(raiderioElementPrefab, raiderioContent.transform);
            createdElement.GetComponent<Image>().sprite = elements[i];
        }
    }

    public void AddRaidbotsElement(List<Sprite> icons)
    {
        if (icons.Count != 5)
        {
            Debug.LogError("ResultsController:AddRaidbotsElement(List<Sprite> elements) : ERROR: Not enough elements.");
            return;
        }

        GameObject createdElement = Instantiate(raidbotsElementPrefab, raidbotsContent.transform);
        createdElement.GetComponent<RaidbotsResultEntry>().SetPlacementText($"{createdElement.transform.GetSiblingIndex() + 1}.");

        for (int i = 0; i < icons.Count; i++)
        { 
            createdElement.GetComponent<RaidbotsResultEntry>().SetIcon(i, icons[i]);
        }
    }

    public void SetDungeonText(string text)
    {
        dungeonText.text = text;
    }

    public void ResetUI()
    {
        for(int i = 0; i < raiderioContent.transform.childCount; i++)
        {
            Destroy(raiderioContent.transform.GetChild(i).gameObject);
        }
        for(int i = 0; i < raidbotsContent.transform.childCount; i++)
        {
            Destroy(raidbotsContent.transform.GetChild(i).gameObject);
        }
        dungeonText.text = "";
    }
}

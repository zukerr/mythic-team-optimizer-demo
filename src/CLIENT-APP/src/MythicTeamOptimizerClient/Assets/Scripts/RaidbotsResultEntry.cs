using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RaidbotsResultEntry : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI placementText = null;
    [SerializeField]
    private GameObject iconsGrid = null;

    public void SetPlacementText(string text)
    {
        placementText.text = text;
    }

    public void SetIcon(int index, Sprite icon)
    {
        iconsGrid.transform.GetChild(index).GetComponent<Image>().sprite = icon;
    }
}

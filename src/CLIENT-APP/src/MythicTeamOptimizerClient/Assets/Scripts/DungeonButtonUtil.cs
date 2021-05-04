using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonButtonUtil : MonoBehaviour
{
    public void SelectDungeon(string dungeonName)
    {
        MainController.Instance.SetUserDungeonChoice(dungeonName);
        SectionController.Instance.OnSelectDungeon();
    }
}

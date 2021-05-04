using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController
{
    private static MainController instance;
    public static MainController Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new MainController();
            }
            return instance;
        }
    }

    private UserInputModel userInputForApiCall;

    private MainController()
    {
        userInputForApiCall = new UserInputModel();
    }

    public void SetUserDungeonChoice(string dungeonName)
    {
        userInputForApiCall.DungeonName = dungeonName;
    }

    public void SetUserClassChoice(string className)
    {
        userInputForApiCall.CharacterClass = className;
    }

    public void SetUserSpecChoice(string specName)
    {
        userInputForApiCall.CharacterSpec = specName;
    }

    public void DebugUserInput()
    {
        Debug.Log(userInputForApiCall);
    }

    public UserInputModel GetUserInput()
    {
        return userInputForApiCall;
    }
}

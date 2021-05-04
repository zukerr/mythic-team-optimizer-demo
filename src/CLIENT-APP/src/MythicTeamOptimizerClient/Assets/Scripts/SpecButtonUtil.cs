using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;

public class SpecButtonUtil : MonoBehaviour
{
    [SerializeField]
    private string className = "";
    [SerializeField]
    private string specName = "";

    public string ClassName => className;
    public string SpecName => specName;

    public void SelectSpec()
    {
        MainController.Instance.SetUserClassChoice(className);
        MainController.Instance.SetUserSpecChoice(specName);
        SectionController.Instance.OnSelectSpec();
        MainController.Instance.DebugUserInput();

        ApiConnector apiConnector = new ApiConnector();
        Task apiRequest = apiConnector.PostRequest(MainController.Instance.GetUserInput());
    }
}

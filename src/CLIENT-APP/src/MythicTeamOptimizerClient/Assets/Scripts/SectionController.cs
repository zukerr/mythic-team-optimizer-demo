using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SectionController : MonoBehaviour
{
    [SerializeField]
    private GameObject dungeonSectionGameobject = null;
    [SerializeField]
    private GameObject specSectionGameobject = null;
    [SerializeField]
    private GameObject loadingScreenGameobject = null;
    [SerializeField]
    private GameObject portSectionGameobject = null;
    [SerializeField]
    private TMP_InputField portInputField = null;
    [SerializeField]
    private SpecDataControl specDataControl = null;
    public SpecDataControl SpecDataControlModule => specDataControl;
    [SerializeField]
    private ResultsController resultsController = null;

    public string RestApiUrl { get; set; }
    private bool dataLoaded = false;
    private string retrievedData = "";

    private static SectionController instance;
    public static SectionController Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    public void OnSelectDungeon()
    {
        dungeonSectionGameobject.SetActive(false);
        //specSectionGameobject.SetActive(true);
    }

    public void OnSelectSpec()
    {
        specSectionGameobject.SetActive(false);
        loadingScreenGameobject.SetActive(true);
        WaitUntilDataLoaded();
    }

    public void OnLinkOk()
    {
        RestApiUrl = portInputField.text;
        portSectionGameobject.SetActive(false);
    }

    public void OnDataLoaded(string data)
    {
        dataLoaded = true;
        retrievedData = data;
    }

    public void WaitUntilDataLoaded()
    {
        StartCoroutine(OnDataLoadedCoroutine());
    }

    private IEnumerator OnDataLoadedCoroutine()
    {
        while(!dataLoaded)
        {
            yield return null;
        }
        Debug.Log("Called on data loaded.");
        ExternalJsonHandler.HandleRetrievedData(resultsController, specDataControl, retrievedData);
        loadingScreenGameobject.SetActive(false);
    }

    public void OnReset()
    {
        specSectionGameobject.SetActive(true);
        dungeonSectionGameobject.SetActive(true);
        portSectionGameobject.SetActive(true);
        resultsController.ResetUI();
        dataLoaded = false;
        retrievedData = "";
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SpecDataControl : MonoBehaviour
{
    [SerializeField]
    private GameObject specsGrid = null;

    public Sprite GetImageOfClassSpec(string className, string specName)
    {
        for(int i = 0; i < specsGrid.transform.childCount; i++)
        {
            SpecButtonUtil temp = specsGrid.transform.GetChild(i).GetComponent<SpecButtonUtil>();
            if(temp.ClassName.Equals(className) && temp.SpecName.Equals(specName))
            {
                Debug.Log("About to return a sprite.");
                return specsGrid.transform.GetChild(i).GetComponent<Image>().sprite;
            }
        }
        Debug.Log("About to return null.");
        return null;
    }
}

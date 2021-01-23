using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTabActions : MonoBehaviour
{
    public GameObject helpWindow;

    public GameObject inputOutputs;
    public Transform parent;

    public void Clear()
    {
        Element[] elems = FindObjectsOfType<Element>();

        foreach (Element elem in elems)
        {
            Destroy(elem.gameObject);
        }
        
    }
    public void Insert()
    {
        Instantiate(inputOutputs, parent);
    }
    public void Save()
    {
        App.Instance.Save();
    }
    public void Load()
    {

    }
    public void Help()
    {
        helpWindow.SetActive(true);
    }

}

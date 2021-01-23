using UnityEngine;
using System.IO;
using UnityEngine.Events;
using Juto;
using System.Collections.Generic;


public class App : Singleton<App>
{
    // (Optional) Prevent non-singleton constructor use.
    protected App() { }

    public UIHandler uiHandler;

    public Color on, off;

    public Camera cam;

    private void Start()
    {
        cam = FindObjectOfType<Camera>();
        uiHandler = FindObjectOfType<UIHandler>();
    }

    /// <summary>
    /// Returns mouse pos to worldpoint
    /// </summary>
    /// <returns></returns>
    public Vector3 Worldpoint()
    {
        Vector3 mousePos = App.Instance.cam.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(mousePos.x, mousePos.y, 0);
    }

    public void Save()
    {
        //Save s = new Save();
        List<SaveStruct> saveElements = new List<SaveStruct>();

        //Load elements
        Element[] elems = FindObjectsOfType<Element>();

        foreach (Element elem in elems)
        {
            SaveStruct s = new SaveStruct();

            s.type = elem.GetType().ToString();
            s.position = elem.transform.position;

            if (s.type == "Gate")
                s.gateType = elem.GetComponent<Gate>().gateType.ToString();
            else if (s.type == "ToggleObj")
                s.state = elem.GetComponent<ToggleObj>().state;

            Debug.Log(s.ToString());
        }

        //JsonUtility.ToJson(s, true);
    }


}


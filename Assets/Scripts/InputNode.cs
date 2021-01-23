using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputNode : MonoBehaviour
{

    public OutputNode receivingFrom;

    private LineRenderer line;

    public int id;

    public bool GetValue()
    {
        if(receivingFrom != null)
            return receivingFrom.GetValue();

        return false;
    }

    public void OnMouseUp()
    {
        App.Instance.uiHandler.StopDragging(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();

        
    }


    void Update()
    {
        if(receivingFrom != null)
        {
            line.positionCount = 2;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, receivingFrom.transform.position);


            Color c = (GetValue()) ? App.Instance.on : App.Instance.off;

            line.startColor = line.endColor = c;
        }
    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(1))
            ClearLine();
    }

    public void ClearLine()
    {
        line.positionCount = 0;
        receivingFrom = null;
    }

    public void ConnectToId(int connectTo)
    {

    }
}

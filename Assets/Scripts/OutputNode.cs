using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutputNode : MonoBehaviour
{

    public object nodeElement;
    public InputNode sendingTo;

    public int id;
    public bool GetValue()
    {
        if(nodeElement != null)
        {
            if (nodeElement.GetType() == typeof(ToggleObj))
            {
                ToggleObj elem = (ToggleObj)nodeElement;
                return elem.state;
            }
            else if (nodeElement.GetType() == typeof(Gate))
            {
                Gate elem = (Gate)nodeElement;
                return elem.output;
            }
            else if(nodeElement.GetType() == typeof(DoubleElement))
            {
                DoubleElement elem = (DoubleElement)nodeElement;
                return elem.GetValue();

            }
        }


        return false;
    }

    public void OnMouseDown()
    {
        App.Instance.uiHandler.StartDragging(this);
    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(1))
            ClearConnection();
    }

    void Start()
    {
        if(GetComponentInParent<ToggleObj>())
        {
            nodeElement = GetComponentInParent<ToggleObj>();
        }
        else if (GetComponentInParent<Gate>() != null)
        {
            nodeElement = GetComponentInParent<Gate>();
        }
        else if (GetComponentInParent<DoubleElement>() != null)
        {
            nodeElement = GetComponentInParent<DoubleElement>();
        }
    }

    public void ClearConnection()
    {
        if (sendingTo != null)
        {
            sendingTo.ClearLine();

            if(sendingTo.receivingFrom != null)
                sendingTo.ClearLine();

            sendingTo = null;

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{

    public bool moving;

    public virtual void Start()
    {
        GetComponent<SpriteRenderer>().color = App.Instance.uiHandler.lineColor;
    }

    public virtual void OnMouseDown()
    {
        if (shiftDown())
        moving = true;

    }

    public virtual void  OnMouseUp()
    {
        moving = false;


    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(1))
            Destroy(gameObject);
    }

    public virtual void OnDestroy()
    {
        OutputNode[] outNode = GetComponentsInChildren<OutputNode>();
        InputNode[] inNode = GetComponentsInChildren<InputNode>();

        foreach (OutputNode node in outNode)
        {
            node.ClearConnection();
        }

        foreach (InputNode node in inNode)
        {
            node.ClearLine();
        }
    }

    public virtual void Update()
    {
        if(moving && !App.Instance.uiHandler.Dragging())
        {
            transform.position = App.Instance.Worldpoint();
        }
    }

    public bool shiftDown()
    {
        return (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
    }

}

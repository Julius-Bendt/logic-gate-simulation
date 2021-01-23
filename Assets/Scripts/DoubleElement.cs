using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleElement : Element
{
    private InputNode input;

   
    public bool GetValue()
    {
        if(input != null)
            return input.GetValue();
        else
        {
            input = GetComponentInChildren<InputNode>();
            return input.GetValue();
        }
    }
}

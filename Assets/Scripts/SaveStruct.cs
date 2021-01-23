using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SaveStruct
{
    public Vector2 position;
    public List<int> inputConnection; //Our input, to output
    public List<int> inputIds, outputIds;
    public string type; //class type (toggle, 
    public string gateType; //used in 
    public bool state; //used in toggle

    public override string ToString()
    {
        return string.Format("Type: {0}, gate: {1}, state: {6}, pos: {2}, inputs: {3} outputs: {4}, connections: \n input: {5}", type, gateType, position, inputIds, outputIds, inputConnection,state);
    }
}

[System.Serializable]
public struct Save
{
    public SaveStruct[] items;
}
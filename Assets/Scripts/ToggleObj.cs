using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObj : Element
{
    public bool state;
    public SpriteRenderer icon;

    public void Toggle()
    {
        state = !state;

        icon.color = (state) ? Color.white : new Color(1, 1, 1, 0.5f);

        GetComponent<SpriteRenderer>().color = (state) ? App.Instance.on : App.Instance.off;
    }

    public override void OnMouseDown()
    {
        base.OnMouseDown();
        Toggle();
    }
}

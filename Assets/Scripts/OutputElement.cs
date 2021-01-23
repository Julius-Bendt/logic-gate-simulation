using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputElement : Element
{
    public InputNode inputNode;

    SpriteRenderer renderer;
    public SpriteRenderer icon;

    public override void Start()
    {
        base.Start();

        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if(inputNode != null)
            renderer.color = (inputNode.GetValue()) ? App.Instance.on : App.Instance.off;

        if(icon != null)
            icon.color = (inputNode.GetValue()) ? Color.white : new Color(1,1,1,0.5f);
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gate : Element
{
    public GateType gateType;
    public InputNode input1;
    public InputNode input2;
    public bool output;
    public OutputNode outputElement;
    private TMP_Dropdown dropdown;
    private RectTransform dropdownRect;
    public Sprite and, or, not;
    private SpriteRenderer iconRendere;


    public enum GateType
    {
        And,
        Or,
        Not
    }

    public override void Start()
    {

        base.Start();

        iconRendere = transform.Find("Type").GetComponent<SpriteRenderer>();

        dropdown = App.Instance.uiHandler.RequestDropdown();

        dropdownRect = dropdown.GetComponent<RectTransform>();

        int selected = 0;



        switch(gateType)
        {
            case GateType.And:
                selected = 0;
                iconRendere.sprite = and;
                break;
            case GateType.Not:
                selected = 1;
                iconRendere.sprite = not;
                input2.gameObject.SetActive(false);
                input1.transform.localPosition = new Vector3(input1.transform.localPosition.x, 0, input1.transform.localPosition.z);
                break;
            case GateType.Or:
                selected = 2;
                iconRendere.sprite = or;
                break;
        }

        dropdown.value = selected;

        dropdown.onValueChanged.AddListener(delegate
        {
            UpdateGateType();
        });
        DisableDropdown();
    }

    public void DisableDropdown()
    {
        dropdown.gameObject.SetActive(false);
    }

    public void EnableDropdown()
    {
        dropdown.gameObject.SetActive(true);


        dropdownRect.position =  new Vector3(transform.position.x + 0.1f, transform.position.y + 0.4f, 1);
    }

    public override void Update()
    {
        base.Update();

        if (outputElement == null || input1 == null)
            return;

        if (gateType != GateType.Not && input2 == null)
            return;

        switch (gateType)
        {
            case GateType.And:
                output = input1.GetValue() && input2.GetValue();
                break;
            case GateType.Or:
                output = input1.GetValue() || input2.GetValue();
                break;
            case GateType.Not:
                output = !input1.GetValue();
                break;
        }
    }

    public override void OnMouseDown()
    {
        base.OnMouseDown();

        if (!App.Instance.uiHandler.Dragging() && shiftDown())
            DisableDropdown();
    }

    public override void OnMouseUp()
    {
        base.OnMouseUp();

        if(!App.Instance.uiHandler.Dragging())
            EnableDropdown();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        Destroy(dropdown.gameObject);
    }

    public void UpdateGateType()
    {

        switch(dropdown.value)
        {
            case 0:
                gateType = GateType.And;
                iconRendere.sprite = and;
                break;
            case 1:
                gateType = GateType.Or;
                iconRendere.sprite = or;
                break;
            case 2:
                gateType = GateType.Not;
                iconRendere.sprite = not;
                break;
        }

        if(gateType == GateType.Not)
        {
            if(input2.receivingFrom != null)
                input2.receivingFrom.ClearConnection();

            input2.ClearLine();
            input2.gameObject.SetActive(false);

            input1.transform.localPosition = new Vector3(input1.transform.localPosition.x, 0, input1.transform.localPosition.z);
        }
        else
        {
            input2.gameObject.SetActive(true);
            input1.transform.localPosition = new Vector3(input1.transform.localPosition.x, 0.276f, input1.transform.localPosition.z);
        }
    }
}


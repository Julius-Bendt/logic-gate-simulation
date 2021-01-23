using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHandler : MonoBehaviour
{

    [Header("Prefabs")]
    public Transform parent;
    public Elements[] elements;
    private GameObject objDragging;

    [Header("Drag bar")]
    public Color lineColor;
    private LineRenderer line;
    private OutputNode currentDragging;

    [Header("Elements bar")]
    public RectTransform toolbar;
    public Vector2 close, open;
    public float pointerThreshold;
    private bool animationActive;
    public AnimationCurve curve;

    [Header("Control bar")]
    public RectTransform controlbar;
    public Vector2 controlClose, controlOpen;
    public float controlPointerThreshold;
    private bool controlAnimationActive;

    [Header("Gate dropdowns")]
    public RectTransform worldCanvas;
    public GameObject dropdown;

    [System.Serializable]
    public struct Elements
    {
        public string search;
        public GameObject obj;
    }

    public bool Dragging()
    {
        return (currentDragging != null);
    }

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        line.startColor = line.endColor = lineColor;
    }


    public void Create(string search)
    {
        foreach (Elements element in elements)
        {
            if(element.search.ToLower() == search.ToLower())
            {
                objDragging = Instantiate(element.obj, parent);
                return;
            }

        }
       
    }


    public void StartDragging(OutputNode node)
    {
        currentDragging = node;
        line.enabled = true;
        line.SetPosition(0, node.transform.position);

    }

    public void StopDragging(InputNode node)
    {
        if(currentDragging != null)
        {
            if(node.receivingFrom != null)
            {
                node.receivingFrom.ClearConnection();
            }

            if(currentDragging.sendingTo != null)
            {
                currentDragging.sendingTo.ClearLine();
            }

            node.receivingFrom = currentDragging;
            currentDragging.sendingTo = node;
            currentDragging = null;
            line.enabled = false;
        }
    }

    public TMP_Dropdown RequestDropdown()
    {
        return Instantiate(dropdown, worldCanvas).GetComponent<TMP_Dropdown>();
    }

    private void Update()
    {
        if(Dragging())
        {
            line.SetPosition(1, App.Instance.Worldpoint());

            if (Input.GetMouseButtonDown(1))
            {
                line.enabled = false;
                currentDragging = null;
            }

        }



        ///element bar
        if(Input.mousePosition.y <= pointerThreshold)
        {
            animationActive = false; //stop all corutines before
            StartCoroutine(Move(open));
        }
        else
        {
            animationActive = false; //stop all corutines before
            StartCoroutine(Move(close));
        }

        ///control bar
        if (Input.mousePosition.y >= Screen.height - controlPointerThreshold)
        {
            controlAnimationActive = false; //stop all corutines before
            StartCoroutine(MoveControlbar(controlOpen));
        }
        else
        {
            controlAnimationActive = false; //stop all corutines before
            StartCoroutine(MoveControlbar(controlClose));
        }

        if (objDragging != null)
        {
            objDragging.transform.position = App.Instance.Worldpoint();

            if (Input.GetMouseButtonDown(0))
            {
                if(objDragging.GetComponentsInChildren<Gate>().Length > 0)
                {
                    Gate[] gates = objDragging.GetComponentsInChildren<Gate>();

                    foreach (Gate g in gates)
                    {
                        g.EnableDropdown();
                    } 
                }

                objDragging = null;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                Destroy(currentDragging);
                currentDragging = null;
            }
                
        }
        
    }

    private IEnumerator Move(Vector3 point)
    {
        float ElapsedTime = 0.0f;
        float time = 0.3f;
        Vector3 startPos = toolbar.anchoredPosition;
        animationActive = true;

        if (Vector2.Distance(startPos, point) < 0.1f)
        {
            animationActive = false;
            ElapsedTime = time;
        }
            

        while (ElapsedTime < time)
        {
            if (toolbar == null)
                break;

            if (!animationActive)
                break;

            ElapsedTime += Time.deltaTime;
            toolbar.anchoredPosition = Vector3.Lerp(startPos, point, curve.Evaluate((ElapsedTime / time)));
            yield return null;
        }

        animationActive = false;

    }

    private IEnumerator MoveControlbar(Vector3 point)
    {
        float ElapsedTime = 0.0f;
        float time = 0.3f;
        Vector3 startPos = controlbar.anchoredPosition;
        controlAnimationActive = true;

        if (Vector2.Distance(startPos, point) < 0.1f)
        {
            controlAnimationActive = false;
            ElapsedTime = time;
        }


        while (ElapsedTime < time)
        {
            if (toolbar == null)
                break;

            if (!controlAnimationActive)
                break;

            ElapsedTime += Time.deltaTime;
            controlbar.anchoredPosition = Vector3.Lerp(startPos, point, curve.Evaluate((ElapsedTime / time)));
            yield return null;
        }

        controlAnimationActive = false;

    }
}

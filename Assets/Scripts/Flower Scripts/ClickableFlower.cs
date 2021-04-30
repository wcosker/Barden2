using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableFlower : MonoBehaviour
{
    [SerializeField]
    private GameObject upArrow = null;
    [HideInInspector]
    private GameObject upArrowInstantiate;

    private bool isClickable = false;
    private Vector2 canvasPos;
    private Canvas canvas;
    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        RectTransform rec = canvas.GetComponent<RectTransform>();

        // Offset position above object bbox (in world space)
        Vector3 offsetPos = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.3f, transform.position.z);

        // Calculate *screen* position (note, not a canvas/recttransform position)
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(offsetPos);

        // Convert screen position to Canvas / RectTransform space <- leave camera null if Screen Space Overlay
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rec, screenPoint, null, out canvasPos);

        //instantiate button and then immeditaley disable it so that you dont have to instantiate and destroy constantly
        upArrowInstantiate = Instantiate(upArrow);
        upArrowInstantiate.transform.SetParent(canvas.transform, false);
        upArrowInstantiate.GetComponent<RectTransform>().localPosition = canvasPos;
        upArrowInstantiate.SetActive(false);
    }

    //when flower is clicked, check to see if script is enabled
    //if script is enabled, disable all of the flowers clickable flower components and reset THIS flowers time
    private void OnMouseDown()
    {
        //if flower is not clickable, return
        if (!isClickable) return;
        //if not, reset the flower clicked times
        GetComponent<Flower>().resetFlowerTime();

        foreach(GameObject flower in GameController.control.getFlowers())
        {
            flower.GetComponent<ClickableFlower>().setClickable(false);
        }
    }

    public void setClickable(bool val)
    {
        isClickable = val;
        upArrowInstantiate.SetActive(val);
    }
}

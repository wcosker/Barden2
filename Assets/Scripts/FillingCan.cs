using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FillingCan : MonoBehaviour
{
    [Header("Player hits this button prefab to fill up bucket")]
    [SerializeField]
    private GameObject waterMe;
    private GameObject tempButton;

    //button action for referring to "addTimeToFlower"
    UnityAction action1;

    private WaterBucket playerWater;

    private int ranSquare;
    void Start()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        RectTransform rec = canvas.GetComponent<RectTransform>();

        // Offset position above object bbox (in world space) (the 1f is the offset)
        float offsetPosY = transform.position.y + 1f;
        Vector3 offsetPos = new Vector3(transform.position.x, offsetPosY, transform.position.z);

        // Calculate *screen* position (note, not a canvas/recttransform position)
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(offsetPos);
        Vector2 canvasPos;
        // Convert screen position to Canvas / RectTransform space <- leave camera null if Screen Space Overlay
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rec, screenPoint, null, out canvasPos);

        //this sets button onClick() function to be THIS FLOWER specifically
        action1 = () => { addWaterToBucket(); };
        //instantiate button and then immeditaley disable it so that you dont have to instantiate and destroy constantly
        tempButton = Instantiate(waterMe);
        tempButton.transform.SetParent(canvas.transform, false);
        tempButton.GetComponent<RectTransform>().localPosition = canvasPos;
        tempButton.GetComponent<Button>().onClick.AddListener(action1);
        tempButton.SetActive(false);

        playerWater = GameObject.FindGameObjectWithTag("Player").GetComponent<WaterBucket>();
    }

    public void addWaterToBucket()
    {
        //if player is about to add water to bucket... then move button
        if (playerWater.fillerUp == 2)
        {
            moveButtonRandomly();
        }
        playerWater.fillBucket();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            tempButton.SetActive(true);
            moveButtonRandomly();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerWater.resetFilling();
            tempButton.SetActive(false);
        }
    }

    private void moveButtonRandomly()
    {
        int prevRan = ranSquare;
        ranSquare = Random.Range(1, 5);
        //this check is to make sure button doesn't move to same location when moving around screen
        //kinda stupid lol but fuck you
        if (prevRan == ranSquare && prevRan == 4) ranSquare--;
        else if (prevRan == ranSquare && prevRan == 1) ranSquare++;
        else if (prevRan == ranSquare) ranSquare += Random.Range(0, 2) * 2 - 1;

        switch (ranSquare)
        {
            case 1:
                tempButton.GetComponent<RectTransform>().position = new Vector3(60, 234, 0);
                break;
            case 2:
                tempButton.GetComponent<RectTransform>().position = new Vector3(60, 60, 0);
                break;
            case 3:
                tempButton.GetComponent<RectTransform>().position = new Vector3(234, 60, 0);
                break;
            case 4:
                tempButton.GetComponent<RectTransform>().position = new Vector3(234, 234, 0);
                break;
        }
    }
}

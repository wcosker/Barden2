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

    // Start is called before the first frame update
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
        playerWater.fillBucket();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            tempButton.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            tempButton.SetActive(false);
        }
    }
}

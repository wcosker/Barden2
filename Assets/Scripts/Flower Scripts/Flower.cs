using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Flower : MonoBehaviour
{
    private const float FLOWERHEALTH = 20f;
    private float currFlowerHp;
    private bool isWatering = false;

    [Header("Player hits this button prefab to water flower")]
    [SerializeField]
    private GameObject waterMe;
    private GameObject tempButton;

    //button action for referring to "addTimeToFlower"
    UnityAction action1;

    public GameObject flowerHpBar;
    private Slider flowerHp;

    //PLAYER'S CURRENT WATER BUCKET
    private WaterBucket playerWater;

    /// <summary>
    /// Button is instantiated here in the correct location with the correct onClick event associated with it, and then is immediately disabled
    /// The location is calculated by getting the screen space that this flower is located on
    /// The button is then enabled when the players collider enters the Flowers
    /// </summary>
    private void Awake()
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
        action1 = () => { addTimeToFlower(); };
        //instantiate button and then immeditaley disable it so that you dont have to instantiate and destroy constantly
        tempButton = Instantiate(waterMe);
        tempButton.transform.SetParent(canvas.transform, false);
        tempButton.GetComponent<RectTransform>().localPosition = canvasPos;
        tempButton.GetComponent<Button>().onClick.AddListener(action1);
        tempButton.SetActive(false);

        //Grabs the flower hp bar at top of screen
        flowerHpBar = GameObject.FindWithTag("FlowerHPBar");
        flowerHp = flowerHpBar.GetComponent<Slider>();

        //sets flower time til death equal to non-changing variable
        currFlowerHp = FLOWERHEALTH;
    }

    private void Start()
    {
        playerWater = GameObject.FindGameObjectWithTag("Player").GetComponent<WaterBucket>();
        flowerHpBar.SetActive(false);
    }

    private void Update()
    {
        currFlowerHp -= Time.deltaTime;
        if (isWatering)
        {
            flowerHp.value = currFlowerHp / FLOWERHEALTH;
        }
    }

    public void addTimeToFlower()
    {
        //if waterBucket has water in it and it doesn't go over max val
        if (playerWater.currentBucket > 0)
        {
            //decrease watering bucket count by 1
            playerWater.waterFlower();
            currFlowerHp += 5f;
            Debug.Log(currFlowerHp);
        }
    }

    //if player touches flower, then show button for watering
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            tempButton.SetActive(true);
            flowerHpBar.SetActive(true);
            isWatering = true;
        }
    }

    //if player exits and was watering then stop
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && isWatering)
        {
            tempButton.SetActive(false);
            flowerHpBar.SetActive(false);
            isWatering = false;
        }
    }
}

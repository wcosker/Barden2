using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFlowerButton : MonoBehaviour
{
    public void activateFlowers()
    {
        GameObject[] flowers;
        flowers = GameObject.FindGameObjectsWithTag("Flower");
        foreach (GameObject flower in flowers)
        {
            flower.GetComponent<ClickableFlower>().setClickableTrue();
        }
    }
}

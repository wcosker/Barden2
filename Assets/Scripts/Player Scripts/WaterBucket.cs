using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBucket : MonoBehaviour
{
    private int bucketSize = 5;
    public float waterAmt = 5f;

    //variable to hold amount of clicks to fill bucket
    public int fillerUp = 0;

    public int currentBucket;
    // Start is called before the first frame update
    void Start()
    {
        currentBucket = bucketSize;
    }

    public void waterFlower()
    {
        currentBucket--;
    }

    public void fillBucket()
    {
        fillerUp++;
        if (fillerUp == 3)
        {
            //perform sound/animation for bucket filling up, update GUI as well
            currentBucket++;
            fillerUp = 0;
            Debug.Log("Added water to bucket");
        }
    }

    public void resetFilling()
    {
        fillerUp = 0;
    }

    public void loseWater()
    {
        //play sound/animation?
        currentBucket--;
    }
}

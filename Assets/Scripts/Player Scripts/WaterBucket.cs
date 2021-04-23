using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBucket : MonoBehaviour
{
    private int bucketSize = 5;
    public float waterAmt = 5f;

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
}

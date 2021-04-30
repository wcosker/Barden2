using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterFlowerButton : MonoBehaviour
{
    //hold player values for watering all/one specific plant
    private int currWaterAll;
    private int currWaterOne;

    private void Start()
    {
        currWaterAll = GameController.control.waterAllButton;
        currWaterOne = GameController.control.waterOneButton;
    }
    public void activateFlowers()
    {
        if (currWaterOne > 0)
        {
            GetComponent<AudioSource>().Play(0);
            foreach (GameObject flower in GameController.control.getFlowers())
            {
                flower.GetComponent<ClickableFlower>().setClickable(true);
            }
            //disable button
            if (currWaterOne == 1)
            {
                GetComponent<Button>().interactable = false;
            }
            currWaterOne--;
        }
    }

    public void waterAll()
    {
        if (currWaterAll > 0)
        {
            GetComponent<AudioSource>().Play(0);
            foreach (GameObject flower in GameController.control.getFlowers())
            {
                flower.GetComponent<Flower>().resetFlowerTime();
            }
            if (currWaterAll == 1)
            {
                GetComponent<Button>().interactable = false;
            }
            currWaterAll--;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFlowerButton : MonoBehaviour
{
    public void activateFlowers()
    {
        GameController.control.setFlowersClickable(true);
    }
}

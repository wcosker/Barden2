using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowFlower : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerMovement>().slowPlayer();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerMovement>().returnSpeed();
        }
    }

    private void Update()
    {
        transform.localScale = new Vector3(7f + Mathf.Sin(Time.time)/2, 7f + Mathf.Sin(Time.time)/2, 7f + Mathf.Sin(Time.time)/2);
    }
}

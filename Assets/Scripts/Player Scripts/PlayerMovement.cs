using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;

    //direction player goes in pertaining to finger location
    private Vector3 dir;
    private float moveSpeed = 8f;

    //finger position on screen
    private Vector3 touchPos;

    //distance allowed between finger and char object
    private const float FINGERLENGTH = 2.5f;
    private float tempFL;

    void Start()
    {
        tempFL = FINGERLENGTH;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if input is detected and NOT touching UI
        if (Input.touchCount > 0 && !EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId) && Input.GetTouch(0).phase != TouchPhase.Ended)
        {
            Touch touch = Input.GetTouch(0);

            //find the WORLD position of the finger and grab it while setting the z axis to zero
            touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            touchPos.z = 0;

            //if the distance between the player and finger is enough then move the player object
            if (Vector3.Distance(touchPos, transform.position) < tempFL)
            {
                dir = (touchPos - transform.position);
                //create velocity towards finger
                rb.velocity = new Vector2(dir.x, dir.y) * moveSpeed;

                //increase allowed finger length over time for UX purposes
                tempFL = 10f;
            }
            else if (rb.velocity.x < 0.01f && rb.velocity.y < 0.01f)
            {
                rb.velocity = Vector2.zero;
            }
            //once movement is completed, reset FINGERLENGTH value and slow down
            else
            {
                tempFL = FINGERLENGTH;
                rb.velocity = rb.velocity * 0.98f;
            }
        }
        else
        {
            tempFL = FINGERLENGTH;
            rb.velocity = rb.velocity * 0.98f;
        }
    }
}

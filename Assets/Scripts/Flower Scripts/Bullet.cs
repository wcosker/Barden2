using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool activeBullet = false;
    [SerializeField]
    private Sprite activatedBullet = null;

    private void Start()
    {
        StartCoroutine(setActive());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && activeBullet)
        {
            collision.GetComponent<WaterBucket>().loseWater();
            Destroy(gameObject);
        }
    }

    private IEnumerator setActive()
    {
        yield return new WaitForSeconds(1.5f);
        activeBullet = true;
        GetComponent<SpriteRenderer>().sprite = activatedBullet;
    }
}

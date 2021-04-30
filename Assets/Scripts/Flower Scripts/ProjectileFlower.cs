using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFlower : MonoBehaviour
{

    [SerializeField]
    private GameObject projectile = null;
    private Transform player;
    public float speed = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        InvokeRepeating("shootProjectile", 1f, 4f);
    }

    private void shootProjectile()
    {
        GameObject tempProj = Instantiate(projectile);
        tempProj.transform.position = transform.position;
        tempProj.GetComponent<Rigidbody2D>().velocity = (player.position - transform.position).normalized * speed;
        Destroy(tempProj, 5f);
    }
}

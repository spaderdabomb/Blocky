using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = Core.FindGameObjectByNameAndTag("Player", "Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Damage(player.GetComponent<Player>().pistolDamage);
            Destroy(gameObject);
        }
    }
}

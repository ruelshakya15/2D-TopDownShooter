using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    player playerScript;//player script
    public int healAmount;//heal amount assigned to each pickup

    public GameObject pickupEffect;//particle effect 

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();//extracting player script at start
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {//if collided object is player
            Instantiate(pickupEffect, transform.position, Quaternion.identity);//instantitate particle effect if collide with player
            playerScript.Heal(healAmount);
            Destroy(this.gameObject);
        }
    }
}

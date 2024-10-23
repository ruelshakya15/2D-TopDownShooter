using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Weapon weaponToEquip;//weapon gameobject selected in inspector according to pickup
    public GameObject pickupEffect;//particle effect for pickup

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")//if picup collides with player
        {
            Instantiate(pickupEffect, transform.position, Quaternion.identity);//effect if collide with player
            collision.GetComponent<player>().ChangeWeapon(weaponToEquip);//call player scrip changeweapon function with weapon to change
            Destroy(gameObject);//destroy current pickup
        }
    }
}

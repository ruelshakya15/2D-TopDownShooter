using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    [HideInInspector]
    public Transform player;//public so derived classes can access it but hidein inspector so it doesn't show up in inspector
    public float speed;
    public float timeBetweenAttacks;
    public int damage;

    public int pickupChance;//weapon pickup drop chance
    public GameObject[] pickups;//array of weapons that may spawn

    public int healthPickupChance;//health pickup drop chance
    public GameObject healthPickup;//actual gameobject that drops

    public GameObject deathEffect;//enemy dying particle effect
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;//get player transform componenet at the start

    }
    public void TakeDamage(int damageAmount)//Damage take function
    {
        health -= damageAmount;

        if (health <= 0)//if enemy dead
        {
            int randomNumber = Random.Range(0, 101);//random number between 0 and 100
            int randHealth = Random.Range(0, 101);//same thing for health pickups as above diff random var for health

            if (randomNumber < pickupChance)//if random no less than our given input pickup change
            {
                GameObject randomPickup = pickups[Random.Range(0, pickups.Length)];//new game object created selected random between 4 pickups(1-4)
                Instantiate(randomPickup, transform.position, transform.rotation);//instantiate this game object where enemy died

            }
            else if (randHealth < healthPickupChance)//weapon pickup vayena vane roll the dice on health pickup
            {
                Instantiate(healthPickup, transform.position, transform.rotation);
            }
            Instantiate(deathEffect, transform.position, Quaternion.identity);//instantiate death effect when destroying enemy//***ASK quarternion.identity=transform.postion
            Destroy(this.gameObject);//destroy gameobject(enemy) if health less than equal to 0
        }
    }

}

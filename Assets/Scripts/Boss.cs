using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public int health;
    public Enemy[] enemies;//enemeis to spawn when boss takes damage
    public float spawnOffset;//so enemies dont spawn exactly where boss is

    private int halfHealth;
    private Animator anim;

    public int damageAmount;

    public GameObject deathEffect;//particle effect for when boss dies
    public GameObject blood;//when boss dies leaves blood on the scene

    private Slider healthbar;//for boss health UI

    private SceneTransitions sceneTransitions;//using sceneTransition class 

    private void Start()
    {
        halfHealth = health / 2;//get halfhealth and animator component at start
        anim = GetComponent<Animator>();
        healthbar = FindObjectOfType<Slider>();//intiallizing our slider variable to point our health slider
        healthbar.maxValue = health;//health bar starting and max values
        healthbar.value = health;
        sceneTransitions = FindObjectOfType<SceneTransitions>();//initializing scene var so it points to a scenetransition script and we 
        //have access to loadScene() function
    }
    public void TakeDamage(int damageAmount)//take damage function same from player with added functionality
    {
        health -= damageAmount;
        healthbar.value = health;
        if (health <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Instantiate(blood, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            healthbar.gameObject.SetActive(false);//remove health bar element from scene
            sceneTransitions.LoadScene("Win");
        }

        if (health <= halfHealth)
        {
            anim.SetTrigger("stage2");//if half health animation transitions to stage2
        }

        Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];//random enemy to spawn
        Instantiate(randomEnemy, transform.position + new Vector3(spawnOffset, spawnOffset, 0), transform.rotation);//note:Vector3 instead of 2 because function only take Vector3 as parameter

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))//if collided with player
        {
            collision.GetComponent<player>().TakeDamage(damageAmount);//extract player script and call takedamage()
        }

    }



}

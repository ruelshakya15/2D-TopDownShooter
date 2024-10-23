using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private player playerScript;
    private Vector2 targetPosition;
    public float speed;
    public int damage;

    public GameObject Effect;//particle effect for when bullet destroys


    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();//extracting player script
        targetPosition = playerScript.transform.position;//players position set as target//**ASK:player position always updates?
        // targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;[ALTERNATE no need playerScript var]
        // targetPosition=playerScript.transform.position;//Yesto garda object ref error

    }

    private void Update()
    {

        if (Vector2.Distance(transform.position, targetPosition) > .1f)//distance between bullet and player significant projectile moves towards player
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            Instantiate(Effect, transform.position, Quaternion.identity);
            Destroy(gameObject);//else bullet reached player's position hence enemy bullet destroyed,****Note:Even if player moves comparision done with
            //position of player when bullet first starts travelling and when it reaches player old position and distance<.1f bullet destroyed.
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//If player and enemy bullet collide call takedamage function of player script
    {
        if (collision.tag == "Player")
        {
            playerScript.TakeDamage(damage);
            // collision.GetComponent<player>().TakeDamage(damage);[ALTERNATIVELY]
            Instantiate(Effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

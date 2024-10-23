using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public GameObject explosion;//for the particle effect 
    public int damage;

    public GameObject soundObject;//for sound
    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);//call function and adds provide delay
        Instantiate(soundObject, transform.position, transform.rotation);
    }

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

    }

    private void DestroyProjectile()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);//What to spawn(spawn particle effect),at current projectile position,and current angle
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {//built in function that store object that we collided with

        if (collision.CompareTag("Enemy"))//if collided object is Enemy tag
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);//Getting Enemy script and calling take damage function with parameter damage amount
            DestroyProjectile();//Destroy projectile if collided with enemy
        }
        if (collision.CompareTag("boss"))
        {
            collision.GetComponent<Boss>().TakeDamage(damage);//Getting boss script and calling take damage function with parameter damage amount
            DestroyProjectile();//Destroy projectile if collided with boss
        }
    }
}

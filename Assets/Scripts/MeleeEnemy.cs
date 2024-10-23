using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public float stopDistance;//since meele enemy chases player 
    private float attackTime;
    public float attackSpeed;
    private void Update()
    {
        if (player != null)
        {//player still alive
            if (Vector2.Distance(transform.position, player.position) > stopDistance)//only work if distance between enemy and player more than stopdistance
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);//like lerp function
            }
            else//if enemy in range of attack 
            {
                if (Time.time >= attackTime)//so we can incoporate timebetweenattaccks (enemy doesn't continously attack)
                {
                    //attack                   
                    attackTime = Time.time + timeBetweenAttacks;
                    StartCoroutine(Attack());//Corroutine so animation doesn't only run for 1 frame
                }
            }
        }
    }

    IEnumerator Attack()//void ko satta IEnumerator
    {
        player.GetComponent<player>().TakeDamage(damage);//getting player script and calling player take damage function so player takes damage

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;

        float percent = 0f;
        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;//variable enable animation of going towards target position and back again acc to percent
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;//wait for next frame and continue execution from this frame
        }

    }

}
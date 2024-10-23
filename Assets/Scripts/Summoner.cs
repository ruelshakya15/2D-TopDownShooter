using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : Enemy
{

    public float minX;//giving max,min X Y so camera follow doesnt exceed background
    public float minY;
    public float maxX;
    public float maxY;

    private Vector2 targetPosition;
    private Animator anim;

    public float timeBetweenSummons;
    private float summonTime;

    public Enemy enemyToSummon;

    public float meleeAttackSpeed;
    public float stopDistance;
    private float meleeAttackTime;

    public override void Start()
    {
        base.Start();//call start function in base class
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        targetPosition = new Vector2(randomX, randomY);//random spawn location for summoner
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (player != null)//player not dead
        {
            if (Vector2.Distance(transform.position, targetPosition) > .5f)//reached desired position or not
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);//if not move toward position and perform run animation
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);//run animation stopped

                if (Time.time >= summonTime)
                {//summon between times check
                    summonTime = Time.time + timeBetweenSummons;
                    anim.SetTrigger("summon");
                }
            }
            if (Vector2.Distance(transform.position, player.position) < stopDistance)//only work if distance between enemy and player less than stopdistance
            {
                if (Time.time >= meleeAttackTime)//so we can incoporate timebetweenattaccks (enemy doesn't continously attack)
                {
                    //attack                   
                    meleeAttackTime = Time.time + timeBetweenAttacks;
                    StartCoroutine(Attack());//Corroutine so animation doesn't only run for 1 frame
                }
            }
        }
    }

    public void Summon()
    {
        if (player != null)
        {
            Instantiate(enemyToSummon, transform.position, transform.rotation);
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
            percent += Time.deltaTime * meleeAttackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;//variable enable animation of going towards target position and back again acc to percent
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;//wait for next frame and continue execution from this frame
        }

    }

}

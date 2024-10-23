using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public float stopDistance;

    private float attackTime;

    private Animator anim;

    public Transform shotPoint;

    public GameObject enemyBullet;//ENEMY SPAWN BULLET

    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

        if (player != null)//*********EtA player variable chai enemy base class bata taneko ho
        {//player still alive
            if (Vector2.Distance(transform.position, player.position) > stopDistance)//only work if distance between enemy and player more than stopdistance
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);//like lerp function
            }

            if (Time.time >= attackTime)//so we can incoporate timebetweenattaccks (enemy doesn't continously attack)
            {
                attackTime = Time.time + timeBetweenAttacks;
                anim.SetTrigger("attack");//attack animation
            }
        }


    }

    public void RangedAttack()
    {
        Vector2 direction = player.position - shotPoint.position;//Now instead of mouse position we look at our shotpoint game object and player postion
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);//rotate on z axis,convert in unity angle
        shotPoint.rotation = rotation;//calculated rotation current GameObject(shot point )to rotate lai assign gareko

        Instantiate(enemyBullet, shotPoint.position, shotPoint.rotation);//Spawn enemy bullet with shotpoint rotation and position
    }
}

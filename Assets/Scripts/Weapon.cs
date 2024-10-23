using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectile;//Spawn projectile object
    public Transform shotPoint;
    public float timeBetweenShots;

    private float shotTime;

    Animator cameraAnim;//for camera shake effect


    // Update is called once per frame
    void Start()
    {
        cameraAnim = Camera.main.GetComponent<Animator>();//gets animator component of main camera
    }
    void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);//rotate on z axis,convert in unity angle
        transform.rotation = rotation;//calculated rotation current GameObject lai assign gareko

        if (Input.GetMouseButton(0))
        {//left mouse button
            if (Time.time >= shotTime)
            {//Delay continouse shooting
                Instantiate(projectile, shotPoint.position, transform.rotation);//Instantiate=spawn,position specified,spawn in current rotation of weapon
                cameraAnim.SetTrigger("shake");//after projectile instantiated shake camera(play shake animation)
                shotTime = Time.time + timeBetweenShots;//we have to wait till timeBetweenshots amount

            }
        }


    }
}

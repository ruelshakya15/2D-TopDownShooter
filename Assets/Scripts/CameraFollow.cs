using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float speed;

    public float minX;//giving max,min X Y so camera follow doesnt exceed background
    public float minY;
    public float maxX;
    public float maxY;

    private void Start()
    {
        transform.position = playerTransform.position;

    }
    private void Update()
    {
        if (playerTransform != null)
        {
            float clampedX = Mathf.Clamp(playerTransform.position.x, minX, maxX);//minx=-5,maxX=10 then we cant exceed 10 and -5
            float clampedY = Mathf.Clamp(playerTransform.position.y, minY, maxY);

            transform.position = Vector2.Lerp(transform.position, new Vector2(clampedX, clampedY), speed);
        }
    }
}

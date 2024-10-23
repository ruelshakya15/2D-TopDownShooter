using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private static Music instance;//same class variable

    private void Awake()
    {//called before start function

        if (instance == null)
        {
            instance = this;//if music instance not assigned(assign the music GameObject attached to this script)
            DontDestroyOnLoad(instance);//dont destroy music gameobject when transitioning scenes
        }
        else//so we dont have multiple music gameobjects
        {
            Destroy(gameObject);
        }

    }
}

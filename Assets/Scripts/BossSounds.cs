using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSounds : MonoBehaviour
{
    private AudioSource source;//for whole audio source component
    public AudioClip[] clips;//audio clip array to be selected

    public float timeBetweenSoundEffects;//Timers for sounds
    private float nextSoundEffectTime;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Time.time >= nextSoundEffectTime)//check current time and next sound time
        {
            int randomNumber = Random.Range(0, clips.Length);//random no 
            source.clip = clips[randomNumber];//audio source's clip sound assign
            source.Play();//play clip
            nextSoundEffectTime = Time.time + timeBetweenSoundEffects;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour
{
    private AudioSource source;//for whole audio source component
    public AudioClip[] clips;//audio clip array to be selected
    private void Start()
    {
        source = GetComponent<AudioSource>();
        int randomNumber = Random.Range(0, clips.Length);//random no 
        source.clip = clips[randomNumber];//audio source's clip sound assign
        source.Play();//play clip
    }
}

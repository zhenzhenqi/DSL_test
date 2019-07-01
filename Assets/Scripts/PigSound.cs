using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PigSound : MonoBehaviour {


    public AudioClip[] clips;
    public float intervalMin = 2f;
    public float intervalMax = 6f;
    AudioSource source;



    float stamp = 0;
    float playInterval;



	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        if (source == null) return;
        //InvokeRepeating("Oink",10f,playInterval);

	}
	
    public void Oink(){
      
        if (GetComponent<PigController>().IsIdle) return;
        if (Time.time - stamp < playInterval) return;
        int index = Random.Range(0, clips.Length);
        source.PlayOneShot(clips[index]);
        //index++;
        //if (index > clips.Length - 1) index = 0;

        playInterval = Random.Range(intervalMin, intervalMax);

        stamp = Time.time;

    }
}

//yzhu319 07/29/2015
using UnityEngine;
using System.Collections;

public class FireballAudio : MonoBehaviour {

	float timer;
	public float timeBetweenAudios = 1.0f;
	AudioSource weaponAudio; 

	// Use this for initialization
	void Start () {
		weaponAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer >= timeBetweenAudios){
			PlayFireAudio();
		}
	}

	void PlayFireAudio(){
		timer = 0.0f;
		weaponAudio.Play ();
	}
}

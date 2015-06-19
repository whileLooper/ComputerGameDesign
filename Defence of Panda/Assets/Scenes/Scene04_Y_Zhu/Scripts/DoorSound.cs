using UnityEngine;
using System.Collections;

public class DoorSound : MonoBehaviour {

	AudioSource doorSoundClip;

	// Use this for initialization
	void Start () {
		doorSoundClip = GetComponent<AudioSource>();
	}


	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Player") == true){
			doorSoundClip.Play ();
		}
	}
}

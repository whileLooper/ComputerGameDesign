// PandaDev
// Shen Yang, Bo Chen, Ryan Diaz, Yuanzheng Zhu

using UnityEngine;
using System.Collections;

public class DoorSound : MonoBehaviour {

	AudioSource doorSoundClip;

	// Use this for initialization
	void Start () {
		doorSoundClip = GetComponent<AudioSource>();
	}


	void OnCollisionEnter(Collision other){
		if(other.gameObject.CompareTag("Player") == true){
			doorSoundClip.Play ();
		}
	}
}

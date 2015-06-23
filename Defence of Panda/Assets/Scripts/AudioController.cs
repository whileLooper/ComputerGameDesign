// PandaDev
// Shen Yang, Bo Chen, Ryan Diaz, Yuanzheng Zhu

using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {
	
	private Animator anim;
	private AudioSource[] audioArray;
	private AudioSource footstepAudio;
	private AudioSource pushBoxAudio;
	//private AudioSource playerFootstepWater;
	AudioSource doorSoundClip;
	
	
	void Start () {
		anim = GetComponent<Animator> ();
		audioArray = GetComponents<AudioSource>();
		
		footstepAudio = audioArray[0];
		pushBoxAudio = audioArray[1];
		doorSoundClip = audioArray[2];
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (anim.GetFloat("Speed") > 0.1) {
			footstepAudio.Play ();
		}
		
		
		if(anim.GetBool("Push")){
			pushBoxAudio.Play ();
		}
	}

	void OnCollisionEnter(Collision other){
		print ("Door collision");
		if(other.gameObject.name != "Floor"){
			doorSoundClip.Play ();
		}
	}
}

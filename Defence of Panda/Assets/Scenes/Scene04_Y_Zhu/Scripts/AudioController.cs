using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {
	
	private Animator anim;
	private AudioSource[] audioArray;
	private AudioSource footstepAudio;
	private AudioSource pushBoxAudio;
	//private AudioSource playerFootstepWater;
	
	
	void Start () {
		anim = GetComponent<Animator> ();
		audioArray = GetComponents<AudioSource>();
		
		footstepAudio = audioArray[0];
		pushBoxAudio = audioArray[1];
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
}

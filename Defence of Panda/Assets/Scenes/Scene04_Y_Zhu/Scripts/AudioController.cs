using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	private Animator anim;
	private AudioSource playerFootstepGrass;
	//private AudioSource playerFootstepWater;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();

		playerFootstepGrass = GetComponent<AudioSource>();
		//playerFootstepWater = audioArray[1];
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (anim.GetFloat("Speed") > 0.01) {
			playerFootstepGrass.Play ();
		}
	}
}

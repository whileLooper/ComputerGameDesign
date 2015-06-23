// PandaDev
// Shen Yang, Bo Chen, Ryan Diaz, Yuanzheng Zhu

using UnityEngine;
using System.Collections;

public class ParticleEffect : MonoBehaviour {

	Animator anim;
	ParticleSystem runParticle;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		runParticle = GetComponentInChildren<ParticleSystem>();
	}
	

	void Update () {
		//particle system position follow palyer position

		if (anim.GetFloat("Speed") > 0.2){
			runParticle.Play ();
		}
	}
	
}

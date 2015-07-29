﻿// PandaDev
// Shen Yang, YuanZheng Zhu, Ryan Diaz, Bo Chen


using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemeyControlScript : MonoBehaviour {

	AudioSource source;
	public AudioClip[] clips;
	Animator anim;
	bool isAttack = false;
	float attackRate = 2.5f;
	float nextAttack = 2.5f;

	public int enemyWorthEach = 10;
	GameObject shackBase;
	GameObject myplayer;
	 
	// Use this for initialization
	void Start () {
		source = gameObject.AddComponent<AudioSource> ();
		anim = GetComponent<Animator> ();
		source.clip = clips[0];
		myplayer = GameObject.FindGameObjectWithTag("Player");

		shackBase = GameObject.FindGameObjectWithTag("Base");
		 
	}
	
	// Update is called once per frame
	void Update () {

		PlayAttack ();

		PlayDeath ();

		PlayRoaring ();
	}

	void PlayAttack (){
		if (anim.GetBool ("Attack") && Time.time > nextAttack) {
			isAttack = !isAttack;
			nextAttack = Time.time + attackRate;
			StartCoroutine(attackSound());
			isAttack = !isAttack;

			//take damage of Base (shack)
			shackBase.GetComponent<BaseHealth>().baseHealth -= 5;
			Debug.Log("Hey, I took 5 damage!");
		}
	}

	void PlayDeath(){
		if (anim.GetBool ("Death")) {
			source.PlayOneShot(clips[1], 0.2f);
			new WaitForSeconds(2.0f);
			StartCoroutine(DestroyEnemy());



		}
	}

	void PlayRoaring() {
		if (anim.GetBool ("Roaring")) {
			source.PlayOneShot(clips[2], 0.2f);
		}
	}

	IEnumerator attackSound() {
		yield return new WaitForSeconds(2.5f);
		source.PlayOneShot (clips [0], 0.2f);
	}

	IEnumerator DestroyEnemy() {
		yield return new WaitForSeconds (3.0f);
		Destroy (gameObject);

		//add money when enemy died
		if(myplayer != null){
			PlaceTurret.money += enemyWorthEach;
		}
	}


}

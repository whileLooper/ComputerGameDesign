//yzhu319, 07/04/2015
using UnityEngine;
using System.Collections;

public class EnemySprayController : MonoBehaviour {

	public Transform enemyTransform;
	public float sprayRange = 5.0f;
	ParticleSystem iceSpray;

	Vector3 sprayStartPos;
	Vector3 sprayTargetPos;
	Vector3 sprayDirection;
	Quaternion sprayArmRotation;
	Animator anim;
	Collider attackCollider;

	void Start(){
		anim = GetComponentInParent<Animator> ();
		attackCollider = GetComponentInParent<CapsuleCollider> ();
		iceSpray = GetComponentInChildren<ParticleSystem>();
		iceSpray.enableEmission = false;
	}

	void Update () {
		OnTriggerEnter (attackCollider);
	}

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Boxes") {
			Debug.Log("In the collider");
			anim.SetTrigger ("Roaring");
			iceSpray.Play();
			SprayIce ();
		}
	}

	void SprayIce(){
		iceSpray.enableEmission = true;
		sprayStartPos = this.transform.position;
		sprayTargetPos = enemyTransform.position;

		sprayDirection = sprayTargetPos - sprayStartPos;
		sprayArmRotation = Quaternion.LookRotation(sprayDirection.normalized);
		this.transform.rotation = sprayArmRotation;
		this.transform.eulerAngles += new Vector3(0f,90f,0f);

//		//within a certain range, attack the enemy
//		if(sprayDirection.magnitude < sprayRange){ 
//			iceSpray.Play ();
//		}
	}
}

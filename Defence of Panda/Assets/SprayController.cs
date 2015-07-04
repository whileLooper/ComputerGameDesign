//yzhu319, 07/04/2015
using UnityEngine;
using System.Collections;

public class SprayController : MonoBehaviour {

	public Transform enemyTransform;
	public float sprayRange = 5.0f;
	ParticleSystem iceSpray;

	Vector3 sprayStartPos;
	Vector3 sprayTargetPos;
	Vector3 sprayDirection;
	Quaternion sprayArmRotation;

	void Start(){
		iceSpray = GetComponentInChildren<ParticleSystem>();
	}

	void Update () {
		SprayIce();
	}

	void SprayIce(){
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

//yzhu319, 07/04/2015
using UnityEngine;
using System.Collections;

public class SprayController : MonoBehaviour {

	Collider[] enemyColliders;
	int enemyMask;
	public float sprayerDamage = 0.1f;
	public float sprayRadius = 5.0f;
	ParticleSystem iceSpray;

	Vector3 sprayStartPos;
	Vector3 sprayTargetPos;
	Vector3 sprayDirection;
	Quaternion sprayArmRotation;

	void Start(){
		iceSpray = GetComponentInChildren<ParticleSystem>();
		enemyMask = LayerMask.GetMask("EnemyLayer");
	}

	void Update () {
		SprayIce();
	}

	void SprayIce(){
		enemyColliders = Physics.OverlapSphere(this.transform.position, sprayRadius, enemyMask);

		//if we have at least one enemy within attacking range
		if(enemyColliders.Length != 0){
			sprayStartPos = this.transform.position;
			sprayTargetPos = enemyColliders[0].gameObject.transform.position;

			sprayDirection = sprayTargetPos - sprayStartPos;
			sprayArmRotation = Quaternion.LookRotation(sprayDirection.normalized);
			this.transform.rotation = sprayArmRotation;
			this.transform.eulerAngles += new Vector3(0f,90f,0f);

			/*Health System*/
			enemyColliders[0].gameObject.GetComponent<EnemyHealth>().enemyHealth -= sprayerDamage;
			//decrease speed;
			enemyColliders[0].gameObject.GetComponent<NavMeshAgent>().speed = 0.4f;
		}

//		//within a certain range, attack the enemy
//		if(sprayDirection.magnitude < sprayRange){ 
//			iceSpray.Play ();
//		}
	}
}

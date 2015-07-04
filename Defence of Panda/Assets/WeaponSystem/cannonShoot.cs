using UnityEngine;
using System.Collections;

public class cannonShoot : MonoBehaviour {

	public float timeBetweenBullets = 1.5f;
	public Transform enemyTransform;
	float timer;
	LineRenderer laser;
	Vector3 laserStartPos;
	Vector3 laserTargetPos;
	Vector3 laserDirection;
	Quaternion laserArmRotation;

	ParticleSystem hitParticle;

	// Use this for initialization
	void Start () {
		laser = GetComponent<LineRenderer>();
		laser.enabled = false;
		hitParticle = GetComponentInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
	
		timer += Time.deltaTime;
		if(timer >= timeBetweenBullets){
			ShootLaser ();
		}
	}

	void ShootLaser(){
		timer = 0.0f;
		laser.enabled = false;
		hitParticle.Stop ();

		laserStartPos = transform.position;
		laserTargetPos = enemyTransform.position + new Vector3(0,1.5f,0);
		laser.SetPosition(0, laserStartPos);
		laser.SetPosition(1, laserTargetPos);

		//rotate the cannon arm to the target position
		laserDirection = laserTargetPos - laserStartPos;
		laserArmRotation = Quaternion.LookRotation(laserDirection.normalized);
		transform.rotation = laserArmRotation;
		transform.eulerAngles += new Vector3(0,-90f,-22f); 

		hitParticle.transform.position = enemyTransform.position + new Vector3(0,1.5f,0);

		//within a certain range, attack the enemy
		if(laserDirection.magnitude < 5.0f){ 

			laser.enabled = true;
			hitParticle.Play ();
		}

	}
}

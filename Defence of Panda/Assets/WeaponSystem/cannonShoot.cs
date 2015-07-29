using UnityEngine;
using System.Collections;

public class cannonShoot : MonoBehaviour {

	public float timeBetweenAudios = 1.0f;
	public float laserRadius = 5.0f;
	public float laserDamage = 0.3f;
	Collider[] enemyColliders;
	int enemyMask;

	float timer;
	LineRenderer laser;
	Vector3 laserStartPos;
	Vector3 laserTargetPos;
	Vector3 laserDirection;
	Quaternion laserArmRotation;

	ParticleSystem hitParticle;

	AudioSource weaponAudio; 
	bool isAttacking;

	// Use this for initialization
	void Start () {

		enemyMask = LayerMask.GetMask("EnemyLayer");

		laser = GetComponent<LineRenderer>();
		laser.enabled = false;
		hitParticle = GetComponentInChildren<ParticleSystem>();
		weaponAudio = GetComponent<AudioSource>();
		isAttacking = false;
	}
	
	// Update is called once per frame
	void Update () {
	
		timer += Time.deltaTime;
		if(timer >= timeBetweenAudios){
			//sound effect when ATTACKing
			if(isAttacking){
				PlayLaserAudio();
			}
		}
		ShootLaser ();
	}

	void PlayLaserAudio(){
		timer = 0.0f;
		weaponAudio.Play ();
	}

	void ShootLaser(){

		enemyColliders = Physics.OverlapSphere(this.transform.position, laserRadius, enemyMask);


		laser.enabled = false;
		isAttacking = false;
		hitParticle.Stop ();

		//within a certain range, attack the enemy
		if (enemyColliders.Length != 0){

			laserStartPos = transform.position;
			//laserTargetPos = enemies[0].transform.position + new Vector3(0,1.5f,0);
			laserTargetPos = enemyColliders[0].gameObject.transform.position + new Vector3(0,1.5f,0);
			laser.SetPosition(0, laserStartPos);
			laser.SetPosition(1, laserTargetPos);

			//rotate the cannon arm to the target position
			laserDirection = laserTargetPos - laserStartPos;
			laserArmRotation = Quaternion.LookRotation(laserDirection.normalized);
			transform.rotation = laserArmRotation;
			transform.eulerAngles += new Vector3(0,-90f,-22f); 

			hitParticle.transform.position =  laserTargetPos;

			laser.enabled = true;
			isAttacking = true;
			hitParticle.Play ();



			/*Health System*/
			enemyColliders[0].gameObject.GetComponent<EnemyHealth>().enemyHealth -= laserDamage;



		}


	}
}

using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	public float startingHealth = 100.0f;
	public float enemyHealth;
	public float damageEachParticleAttack = 2.0f;
	Animator anim;


	public GameObject healthBar;
	public GameObject healthBarBase;
	Vector3 healthBarZeroPoint; //left point of healthbar base
	float barLengthInit;
	float barLength;
	NavMeshAgent navAgent;


	// Use this for initialization
	void Awake () {
		enemyHealth = startingHealth;
		anim = GetComponent<Animator>();
		barLengthInit = healthBar.GetComponent<Renderer>().bounds.extents.x; //this is half-length of inital healthbar
		barLength = barLengthInit;
		navAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
	
		//fix orientation of healthBar and healthBarBase
		healthBar.transform.eulerAngles = new Vector3(90f, 90f, 0);
		healthBarBase.transform.eulerAngles = new Vector3(90f, 90f, 0);



		Debug.Log(enemyHealth/startingHealth);

		//shrink health bar length based on currentHealth/startingHealth ratio
		healthBar.transform.localScale = new Vector3(0.1f,0.5f * Mathf.Clamp(enemyHealth/startingHealth, 0, 1.0f),0.1f);
		//move healthBar to the left end
		barLength = enemyHealth/startingHealth * barLengthInit;
		healthBar.transform.position = healthBarBase.transform.position + new Vector3((barLength - barLengthInit),0,0);

		if(enemyHealth <= 0){
			anim.SetTrigger ("Death");
			//object destroyed in enemy manager script

			//disable enemy navigation
			navAgent.speed = 0;

		}
	}

	void OnParticleCollision()
	{
		Debug.Log("My Enemy's been hit by particles.");
		enemyHealth -= damageEachParticleAttack;

	}
}

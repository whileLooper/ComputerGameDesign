using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	public float startingHealth = 100.0f;
	public float enemyHealth;
	public float damageEachParticleAttack = 2.0f;
	Animator anim;
	// Use this for initialization
	void Awake () {
		enemyHealth = startingHealth;
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
		if(enemyHealth <= 0){
			anim.SetTrigger ("Dead");
			Destroy(gameObject);
		}
	}

	void OnParticleCollision()
	{
		Debug.Log("My Enemy's been hit by particles.");
		enemyHealth -= damageEachParticleAttack;
	}
}

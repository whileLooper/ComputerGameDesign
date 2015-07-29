using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BaseHealth : MonoBehaviour {

	public float startingHealth = 100.0f;
	public float baseHealth;

	public Animator anim;
	public Slider baseHealthSlider;

	//public float damageEachEnemyAttack = 2.0f;

	void Awake () {
		baseHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		baseHealthSlider.value = baseHealth;

		if (baseHealth <= 0){
			//Game Over scripts here
			Debug.Log ("GameOver\n");
			anim.SetTrigger("GameOver");
			//Application.LoadLevel (Application.loadedLevel);
		}
		/*if (Input.GetKey (KeyCode.K)) {
			anim.SetTrigger("GameOver");
		}*/
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.CompareTag("Enemy")){
			other.gameObject.GetComponent<Animator>().SetTrigger("Attack");
		}
	}
}

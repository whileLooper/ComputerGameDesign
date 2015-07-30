//yzhu319 07/27/2015
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

	public Text waveText;

	public GameObject enemyAlpha;
	public GameObject enemyBeta;

	public GameObject spawnPoint1;
	public GameObject spawnPoint2;

	public BaseHealth baseHealth;
	public Animator anim;

	//public float spawnTime = 5f;
	public float beginWaitTime = 10f;
	public float waveWaitTime12 = 20f;
	public float waveWaitTime23 = 40f;

	public float enemyInterval1 = 10f;
	public float enemyInterval2 = 6f;
	public float enemyInterval3 = 10f;

	public AudioClip[] clips;

	AudioSource source;
	Color waveTextColor; 
	float textAlpha;

	void Start(){
		if (waveText == null){
			Debug.LogError("Cannot find wave Text");
		}
		waveTextColor = waveText.color;
		textAlpha = (float)(waveTextColor.a);
		source = GetComponent<AudioSource> ();
		source.clip = clips [0];
		source.Play ();
		StartCoroutine(SpawnEnemies ());
	}

	IEnumerator SpawnEnemies ()
	{
		waveText.text = "Prepare your defence ...";

		yield return new WaitForSeconds(beginWaitTime);

		FadeOut ();
		source.clip = clips [1];
		source.Play ();
		source.loop = true;
		Debug.Log("Wave 1 begins" + Time.time);
		waveText.text = "<<< Wave 1 coming ...";

		FadeIn ();


		for(int i = 0; i < 5; i++){
			Instantiate (enemyAlpha, spawnPoint1.transform.position, spawnPoint1.transform.rotation);
			yield return new WaitForSeconds(enemyInterval1);
		}
		Debug.Log("Wave 1 ends" + Time.time);
		FadeOut ();

		yield return new WaitForSeconds(waveWaitTime12);

		waveText.text = "Wave 2 coming ... >>>";

		FadeIn ();


		Debug.Log("Wave 2 begins" + Time.time);
		for(int i = 0; i < 10; i++){
			Instantiate (enemyBeta, spawnPoint2.transform.position, spawnPoint2.transform.rotation);
			yield return new WaitForSeconds(enemyInterval2);
		}
		Debug.Log("Wave 2 ends" + Time.time);
		FadeOut ();

		yield return new WaitForSeconds(waveWaitTime23);

		waveText.text = "<<< Final wave coming ... >>>";

		FadeIn ();


		Debug.Log("Wave 3 begins" + Time.time);
		for(int i = 0; i < 10; i++){
			Instantiate (enemyAlpha, spawnPoint1.transform.position, spawnPoint1.transform.rotation);
			Instantiate (enemyBeta, spawnPoint2.transform.position, spawnPoint2.transform.rotation);
			yield return new WaitForSeconds(enemyInterval3);
		}
		Debug.Log("Wave 3 ends" + Time.time);
		FadeOut ();
		//yield return new WaitForSeconds (10);
		int enemies = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		while (enemies > 0) {
			yield return new WaitForSeconds(3);
			enemies = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		}
		checkBase ();

	}

	void checkBase() {

		if (baseHealth.baseHealth > 0) {
			anim.SetTrigger("Win");
		}
	}

	void FadeIn(){
		while(textAlpha <= 1){
			textAlpha += .1f * Time.deltaTime * 2;
		}
	}

	void FadeOut(){
		while(textAlpha >= 0){
			textAlpha -= .1f * Time.deltaTime * 2;
		}
	}
}

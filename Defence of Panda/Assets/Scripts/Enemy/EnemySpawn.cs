using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {
	
	public GameObject enemyAlpha;
	public GameObject enemyBeta;

	public GameObject spawnPoint1;
	public GameObject spawnPoint2;

	//public float spawnTime = 5f;
	public float beginWaitTime = 10f;
	public float waveWaitTime12 = 20f;
	public float waveWaitTime23 = 10f;

	public float enemyInterval1 = 8f;
	public float enemyInterval2 = 6f;
	public float enemyInterval3 = 4f;

	void Start(){
		StartCoroutine(SpawnEnemies ());
	}

	IEnumerator SpawnEnemies ()
	{
		yield return new WaitForSeconds(beginWaitTime);


		//InvokeRepeating ("Spawn", spawnTime, spawnTime);
		Debug.Log("Wave 1 begins" + Time.time);
		for(int i = 0; i < 5; i++){
			Instantiate (enemyAlpha, spawnPoint1.transform.position, spawnPoint1.transform.rotation);
			yield return new WaitForSeconds(enemyInterval1);
		}
		Debug.Log("Wave 1 ends" + Time.time);

		yield return new WaitForSeconds(waveWaitTime12);


		Debug.Log("Wave 2 begins" + Time.time);
		for(int i = 0; i < 10; i++){
			Instantiate (enemyBeta, spawnPoint2.transform.position, spawnPoint2.transform.rotation);
			yield return new WaitForSeconds(enemyInterval2);
		}
		Debug.Log("Wave 2 ends" + Time.time);

		yield return new WaitForSeconds(waveWaitTime23);

		Debug.Log("Wave 3 begins" + Time.time);
		for(int i = 0; i < 10; i++){
			Instantiate (enemyAlpha, spawnPoint1.transform.position, spawnPoint1.transform.rotation);
			Instantiate (enemyBeta, spawnPoint2.transform.position, spawnPoint2.transform.rotation);
			yield return new WaitForSeconds(enemyInterval3);
		}
		Debug.Log("Wave 3 ends" + Time.time);

	}
	
	
//	void SpawnWave1 ()
//	{
//		for(int i = 0; i < 5; i++){
//			Instantiate (enemy, this.transform.position, this.transform.rotation);
//			StartCoroutine (WaitBtwEnemy1 ());
//		}
//	}
//
//
//	void SpawnWave2 ()
//	{
//		for(int i = 0; i < 10; i++){
//			Instantiate (enemy, this.transform.position, this.transform.rotation);
//			StartCoroutine (WaitBtwEnemy2 ());
//		}
//	}
//
//	void SpawnWave3 ()
//	{
//		for(int i = 0; i < 15; i++){
//			Instantiate (enemy, this.transform.position, this.transform.rotation);
//			StartCoroutine (WaitBtwEnemy3 ());
//		}
//	}

//
//	IEnumerator WaitAtBeginning() {
//		yield return new WaitForSeconds(beginWaitTime);
//	}
//
//	IEnumerator WaitBtwWave12 (){
//		yield return new WaitForSeconds(waveWaitTime12);
//	}
//
//	IEnumerator WaitBtwWave23 (){
//		yield return new WaitForSeconds(waveWaitTime23);
//	}
//
//	IEnumerator WaitBtwEnemy1 (){
//		yield return new WaitForSeconds(enemyInterval1);
//	}
//
//	IEnumerator WaitBtwEnemy2 (){
//		yield return new WaitForSeconds(enemyInterval2);
//	}
//
//	IEnumerator WaitBtwEnemy3 (){
//		yield return new WaitForSeconds(enemyInterval3);
//	}

}

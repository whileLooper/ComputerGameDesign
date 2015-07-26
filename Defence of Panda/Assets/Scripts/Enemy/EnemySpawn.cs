using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {
	
	public GameObject enemy;
	public float spawnTime = 5f;
	public float beginTime = 15f;


	void Start ()
	{
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}
	
	
	void Spawn ()
	{
//		if(baseHealth.currentHealth <= 0f)
//		{
//			return;
//		}
		if (Time.time >= beginTime) {
			print ("This should spawn");
			Instantiate (enemy, this.transform.position, this.transform.rotation);
		}
	}


}

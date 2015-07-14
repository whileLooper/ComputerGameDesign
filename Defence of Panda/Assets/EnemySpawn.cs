using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {
	
	public GameObject enemy;
	public float spawnTime = 5f;


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

		Instantiate (enemy, this.transform.position, this.transform.rotation);
	}


}

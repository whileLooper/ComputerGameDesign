using UnityEngine;
using System.Collections;

public class EnemyNavigation : MonoBehaviour {

	GameObject shack;
	NavMeshAgent navAgent;
	Vector3 offset;

	void Awake () {
		shack = GameObject.FindGameObjectWithTag ("Base");
		navAgent = GetComponent<NavMeshAgent>();
		offset = new Vector3(0,0,-1.5f);
	}

	void Update(){
		navAgent.SetDestination (shack.transform.position + offset);

	}
}

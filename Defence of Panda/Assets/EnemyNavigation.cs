using UnityEngine;
using System.Collections;

public class EnemyNavigation : MonoBehaviour {

	GameObject shack;
	NavMeshAgent navAgent;

	void Awake () {
		shack = GameObject.FindGameObjectWithTag ("Base");
		navAgent = GetComponent<NavMeshAgent>();
	}

	void Update(){
		navAgent.SetDestination (shack.transform.position);

	}
}

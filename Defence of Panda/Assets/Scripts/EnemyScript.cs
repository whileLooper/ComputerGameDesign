using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	public static float healthPoint;
	Animator anim;
	// Use this for initialization
	void Start () {
		healthPoint = 100f;
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		healthPoint = anim.GetFloat("HealthPoint");
	}

	public static void setHealthPoint (float num) {
		healthPoint += num;
	}
}

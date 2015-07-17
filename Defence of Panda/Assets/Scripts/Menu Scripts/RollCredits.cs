using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RollCredits : MonoBehaviour {

	public Text text;
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = text.GetComponent<Animator> ();
	}
	void Update() {

	}

	public void creditsRoll() {
		anim.SetBool ("Credits", true);
	}

	public void creditsStop() {
		anim.SetBool ("Credits", false);
	}
}

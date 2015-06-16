using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float animSpeed = 1.5f;
	Vector3 input;
	Animator anim;
	bool running = false;
	public float moveSpeed = 2f;

	static int jumpState = Animator.StringToHash("Base Layer.Jump");
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		//col = GetComponent<CapsuleCollider> ();
	}
	
	void FixedUpdate () {
		float h = Input.GetAxis("Horizontal");				// setup h variable as our horizontal input axis
		float v = Input.GetAxis("Vertical");				// setup v variables as our vertical input axis
		anim.SetFloat("Speed", v);							// set our animator's float parameter 'Speed' equal to the vertical input axis				
		anim.SetFloat("Direction", h); 						// set our animator's float parameter 'Direction' equal to the horizontal input axis		
		anim.speed = animSpeed;								// set the speed of our animator to the public variable 'animSpeed'
		//currentBaseState = anim.GetCurrentAnimatorStateInfo(0);	// set our currentState variable to the current state of the Base Layer (0) of animation
		if (anim.GetFloat("Speed") > 0.1) {
			running = true;
		}
		if (running) {
			transform.Translate (Vector3.forward * moveSpeed);
			running = false;
		}

		if (Input.GetButtonDown ("Jump")) {
			anim.SetBool("Jump", true);
		}
	}
}

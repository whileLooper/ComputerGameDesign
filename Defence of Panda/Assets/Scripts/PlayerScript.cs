// PandaDev
// Shen Yang, Bo Chen, Ryan Diaz, Yuanzheng Zhu

using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	Vector3 input;
	Animator anim;
	private AnimatorStateInfo currentBaseState;			// a reference to the current state of the animator, used for base layer
	
	public GameObject ragdoll;
	GameObject currentObject;
	private CapsuleCollider col;
	public Transform deathPos;
	
	public float moveSpeed = 0.3f;
	public float animSpeed = 1.5f;
	
	public bool useCurve;
	bool collision = false;
	bool push = false;
	bool running = false;
	bool boxInRange = false;


	static int idleState = Animator.StringToHash("Base Layer.Idle");
	static int jumpState = Animator.StringToHash("Base Layer.Jump");


	float [][] map;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		col = GetComponent<CapsuleCollider> ();
	}
	
	void FixedUpdate () {
		float h = Input.GetAxis ("Horizontal");				// setup h variable as our horizontal input axis
		float v = Input.GetAxis ("Vertical");				// setup v variables as our vertical input axis
		anim.SetFloat ("Speed", v);							// set our animator's float parameter 'Speed' equal to the vertical input axis				
		anim.SetFloat ("Direction", h);

		anim.speed = animSpeed;								// set the speed of our animator to the public variable 'animSpeed'
		//currentBaseState = anim.GetCurrentAnimatorStateInfo(0);	// set our currentState variable to the current state of the Base Layer (0) of animation
		anim.SetBool ("Jump", false);
		anim.SetBool ("Push", false);

		if (anim.GetFloat ("Speed") > 0.1) {
			transform.Translate (Vector3.forward * moveSpeed);
		}
		if (anim.GetFloat ("Speed") < -0.1) {
			transform.Translate (Vector3.forward * moveSpeed);
		}

		if (anim.GetFloat ("Direction") > 0.1) {
			transform.Translate (Vector3.forward * moveSpeed);
		}

		if (anim.GetFloat ("Direction") < -0.1) {
			transform.Translate (Vector3.forward * moveSpeed);
		}
		if (Input.GetKeyDown ("space")) {
			Debug.Log ("space key was pressed");
			anim.SetBool("Jump", true);

		}

		if (Input.GetKeyDown(KeyCode.F1)) {
			Application.LoadLevel(0);
		}
		if (Input.GetKeyDown(KeyCode.F2)) {
			Application.LoadLevel(1);
		}
		if (Input.GetKeyDown(KeyCode.F3)) {
			Application.LoadLevel(2);
		}
		if (Input.GetKeyDown(KeyCode.F4)) {
			Application.LoadLevel(3);
		}

		// Check for collision with the block and press e to do animation when collided.
		if (Input.GetKeyDown ("e")) {
			push = true;
			anim.SetBool ("Push", true);
			if (collision && currentObject.CompareTag("Boxes") == true) {
				print ("Collided with a box!");
				StartCoroutine(WaitTwoSeconds());
				collision = false;
			}
		}

		// if we are currently in a state called Locomotion (see line 25), then allow Jump input (Space) to set the Jump bool parameter in the Animator to true
		if (currentBaseState.nameHash == idleState) {
			Debug.Log ("in idle state");
			if (Input.GetButtonDown ("Jump")) {
				anim.SetBool ("Jump", true);
			}
		}
		
		// if we are in the jumping state... 
		else if (currentBaseState.nameHash == jumpState) {
			Debug.Log("in jump state");
			//  ..and not still in transition..
			if (!anim.IsInTransition (0)) {
				
				if (useCurve)
					// ..set the collider height to a float curve in the clip called ColliderHeight
					col.height = anim.GetFloat ("ColliderHeight");
				
				// reset the Jump bool so we can jump again, and so that the state does not loop 
				anim.SetBool ("Jump", false);
			}
		}


		// Character drops dead on G key
		if (Input.GetKeyDown (KeyCode.G)) {
			//Instantiate(ragdoll, deathPos.transform.position, deathPos.transform.rotation);
			GameObject rag = GameObject.Instantiate(ragdoll, deathPos.transform.position, 
			                                        deathPos.transform.rotation) as GameObject;
			CopyTransformRecurse(deathPos, rag.transform);

			gameObject.SetActive(false);
		}

		if (h != 0f || v != 0f) {
			Vector3 targetDirection = new Vector3(h, 0f, v);
			Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
			Quaternion newRotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, targetRotation, 15f * Time.deltaTime);
			GetComponent<Rigidbody>().MoveRotation(newRotation);

		}
	}

	// Function to set Ragdoll in player's pose
	// Recursively copies each object's transform position and rotation
	void CopyTransformRecurse(Transform src, Transform dst) {
		dst.position = src.position;
		dst.rotation = src.rotation;
		dst.gameObject.SetActive (true);
		
		foreach (Transform child in dst) {
			var curSrc = src.Find(child.name);
			if (curSrc){
				CopyTransformRecurse (curSrc, child);
			}

		}

	}

	void OnCollisionEnter(Collision other) {
		print ("Collided with something " + other.gameObject);
		currentObject = other.gameObject;
		if (other.gameObject.name != "Floor") {
			collision = true;
		}
	}

	IEnumerator WaitTwoSeconds() {
		print(Time.time);
		yield return new WaitForSeconds(1);
		print (currentObject.name);
		currentObject.transform.Translate (transform.forward * 1f);
		print(Time.time);
	}
}

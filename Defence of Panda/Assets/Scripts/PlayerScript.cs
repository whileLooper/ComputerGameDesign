using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float animSpeed = 1.5f;
	Vector3 input;
	Animator anim;
	bool running = false, collision = false, push = false;
	public float moveSpeed = 1f;

	public GameObject ragdoll;
	public Transform deathPos;
	bool hello = false;
	static int jumpState = Animator.StringToHash("Base Layer.Jump");
	bool boxInRange = false;

	GameObject currentObject;
	private CapsuleCollider col;

	float [][] map;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		col = GetComponent<CapsuleCollider> ();
		col.height = 2;
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
		anim.SetBool ("Left", false);

		if (anim.GetFloat ("Speed") > 0.1) {
			running = true;
		}
		if (running) {
			transform.Translate (Vector3.forward * moveSpeed);
			running = false;
		}


		if (Input.GetKeyDown ("space")) {
			Debug.Log ("space key was pressed");
			anim.SetBool("Jump", true);
			col.height = anim.GetFloat("ColliderHeight");

		}

		// commented this off because it isn't doing anything but making the player fall into the ground.
		/*
		if (!anim.IsInTransition (0)) {
			col.height = anim.GetFloat("ColliderHeight");
		}
		*/

		// Check for collision with the block and press e to do animation when collided.
		if (collision && currentObject.name == "Cube") {
			//collision = false;
			if (Input.GetKeyDown ("e")) {
				push = true;
				anim.SetBool ("Push", true);
				print ("hello");
			}
		}

		// Wait for 2 seconds before moving the block
		if (anim.GetBool ("Push") == true) {
			StartCoroutine(WaitTwoSeconds());
			collision = false;
		}





		// Character turns left on A key
		if (Input.GetKeyDown (KeyCode.Q)) {
			Vector3 targetDirection = new Vector3(0, 0f, 3f);
			
			// Create a rotation based on this new vector assuming that up is the global y axis.
			Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
			
			// Create a rotation that is an increment closer to the target rotation from the player's rotation.
			Quaternion newRotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, targetRotation, 15f * Time.deltaTime);
			
			// Change the players rotation to this new rotation.
			GetComponent<Rigidbody>().MoveRotation(newRotation);
		}
		if (h != 0f || v != 0f) {
			//anim.SetBool ("Left", true);
			//transform.Rotate(0, -90, 0);
			//StartCoroutine (TurnLeft());
			// Create a new vector of the horizontal and vertical inputs.
			Vector3 targetDirection = new Vector3(h, 0f, v);
			
			// Create a rotation based on this new vector assuming that up is the global y axis.
			Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
			
			// Create a rotation that is an increment closer to the target rotation from the player's rotation.
			Quaternion newRotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, targetRotation, 15f * Time.deltaTime);
			
			// Change the players rotation to this new rotation.
			GetComponent<Rigidbody>().MoveRotation(newRotation);

		}
	}

	void Update() {
		// Character drops dead on G key
		if (Input.GetKeyDown (KeyCode.G)) {
			//Instantiate(ragdoll, deathPos.transform.position, deathPos.transform.rotation);
			GameObject rag = GameObject.Instantiate(ragdoll, deathPos.transform.position, 
			                                        deathPos.transform.rotation) as GameObject;
			CopyTransformRecurse(deathPos, rag.transform);
			
			gameObject.SetActive(false);
		}

	}

	// Function to set Ragdoll in player's pose
	// Recursively copies each object's transform position and rotation
	void CopyTransformRecurse(Transform src, Transform dst) {
		dst.position = src.position;
		dst.rotation = src.rotation;
		dst.gameObject.SetActive (true);
		
		foreach (Transform child in dst) {
			var curSrc = src.Find (child.name);
			if (curSrc) {
				CopyTransformRecurse (curSrc, child);
			}

		}
	}
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Box") {
			Debug.Log("in box range");
			boxInRange = true;
		}
	}
	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Box") {
			Debug.Log("out of box range");
			boxInRange = false;
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
		currentObject.transform.Translate (Vector3.forward * 2f);
		print(Time.time);
	}

	IEnumerator TurnLeft() {
		yield return new WaitForSeconds(0.6f);
		transform.Rotate(0, -90, 0);
	}

	IEnumerator ChangeToIdle() {
		yield return new WaitForSeconds (2);
		anim.SetBool ("Left", false);
	}
}

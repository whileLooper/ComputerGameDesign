// PandaDev
// Shen Yang, Bo Chen, Ryan Diaz, Yuanzheng Zhu

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	private static Vector3 UP = new Vector3 (0f, 0f, 1f);
	private static Vector3 DOWN = new Vector3 (0f, 0f, -1f);
	private static Vector3 LEFT = new Vector3 (-1f, 0f, 0f);
	private static Vector3 RIGHT = new Vector3 (1f, 0f, 0f);
	private Vector3 playerPos;

	private static float healthPoint = 100;

	Vector3 input;
	Animator anim;
	private AnimatorStateInfo currentBaseState;			// a reference to the current state of the animator, used for base layer
	
	public GameObject ragdoll;
	GameObject currentObject;
	private CapsuleCollider col;
	public Transform deathPos;
	private Rigidbody body;

	//Box moving variables
	public int boxCount = 10;
	public float rayLength = 1.1f;
	public GameObject box;
	public Text boxText;
	private RaycastHit hit;
	private Vector3 rayStart;
	private Vector3 boxPos;

	private Vector3 targetDirection;
	private Vector3 targetDirection2;

	public float moveSpeed = 4f;
	public float animSpeed = 1.5f;
	
	public bool useCurve;
	bool collision = false;
	bool push = false;
	bool running = false;
	bool boxInRange = false;


	static int idleState = Animator.StringToHash("Base Layer.Idle");
	static int jumpState = Animator.StringToHash("Base Layer.Jump");

	public bool oldMove;
	float [][] map;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		col = GetComponent<CapsuleCollider> ();
		playerPos = transform.position;
		oldMove = true;
		body = GetComponent<Rigidbody> ();
	}

	void Update () {


		anim.speed = animSpeed;								// set the speed of our animator to the public variable 'animSpeed'
		//currentBaseState = anim.GetCurrentAnimatorStateInfo(0);	// set our currentState variable to the current state of the Base Layer (0) of animation
		anim.SetBool ("Jump", false);
		anim.SetBool ("Push", false);
		//anim.SetBool ("Moving", false);

		if (Input.GetKeyDown (KeyCode.P)) {
			oldMove = !oldMove;
		}

		if (oldMove) {

			float h = Input.GetAxis ("Horizontal");				// setup h variable as our horizontal input axis
			float v = Input.GetAxis ("Vertical");				// setup v variables as our vertical input axis
			anim.SetFloat ("Speed", v);							// set our animator's float parameter 'Speed' equal to the vertical input axis				
			anim.SetFloat ("Direction", h);
			if (v != 0 || h != 0) {
				anim.SetBool("Moving", true);
			}
			else {
				anim.SetBool("Moving", false);
			}
			if (h != 0f || v != 0f) {
				targetDirection = new Vector3 (h, 0f, v);
				Quaternion targetRotation = Quaternion.LookRotation (targetDirection, Vector3.up);
				Quaternion newRotation = Quaternion.Lerp (body.rotation, targetRotation, 20f * Time.deltaTime);
				body.MoveRotation (newRotation);
			}
			if (v > 0.1) {
				transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
			} else if (v < -0.1) {
				transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
			} else if (h > 0.1) {
				transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
			} else if (h < -0.1) {
				transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
			}

		}
		else {
			if (Input.GetKeyDown (KeyCode.W)) {
				if (targetDirection2 == UP) {
					if (!Physics.Raycast (transform.position + new Vector3 (0f, 0.5f, 0f), 
				                     transform.forward, out hit, rayLength)) {
						playerPos += UP;
					}
				} else {
					targetDirection2 = UP;
				}
			}
			if (Input.GetKeyDown (KeyCode.A)) {
				if (targetDirection2 == LEFT) {
					if (!Physics.Raycast (transform.position + new Vector3 (0f, 0.5f, 0f), 
				                     transform.forward, out hit, rayLength)) {
						playerPos += LEFT;
					}
				} else {
					targetDirection2 = LEFT;
				}
			}
			if (Input.GetKeyDown (KeyCode.S)) {
				if (targetDirection2 == DOWN) {
					if (!Physics.Raycast (transform.position + new Vector3 (0f, 0.5f, 0f), 
				                     transform.forward, out hit, rayLength)) {
						playerPos += DOWN;
					}
				} else {
					targetDirection2 = DOWN;
				}
			}
			if (Input.GetKeyDown (KeyCode.D)) {
				if (targetDirection2 == RIGHT) {
					if (!Physics.Raycast (transform.position + new Vector3 (0f, 0.5f, 0f), 
				                     transform.forward, out hit, rayLength)) {
						playerPos += RIGHT;
					}
				} else {
					targetDirection2 = RIGHT;
				}
			}
			if (playerPos != transform.position) {
				anim.SetBool ("Moving", true);
				transform.position = Vector3.MoveTowards (transform.position, playerPos, moveSpeed * Time.deltaTime);
			}
			else {
				anim.SetBool ("Moving", false);
			}
			if (targetDirection2 != transform.forward) {
				Quaternion targetRotation = Quaternion.LookRotation(targetDirection2, Vector3.up);
				Quaternion newRotation = Quaternion.Lerp(body.rotation, targetRotation, 15f * Time.deltaTime);
				body.MoveRotation(newRotation);
			}
		}



//		if (Input.GetKeyDown ("space")) {
//			Debug.Log ("space key was pressed");
//			anim.SetBool("Jump", true);
//
//		}

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
		/*if (Input.GetKeyDown ("e")) {
			push = true;
			anim.SetBool ("Push", true);
			print ("Not boxes: + " + currentObject.tag);
			if (collision && currentObject.CompareTag("Boxes") == true) {
				print (currentObject.tag);
				StartCoroutine(WaitTwoSeconds());
				collision = false;
			}
		}*/

//		boxText.text = "Boxes: " + boxCount;
		// Handles Picking up/Dropping Boxes on pressing E
		if (Input.GetKeyDown ("e")) {
			if (Physics.Raycast(transform.position + new Vector3(0f, 0.5f, 0f), transform.forward, out hit, rayLength)) {
				print (hit.transform.gameObject.tag);
				if (hit.collider.gameObject.CompareTag("Boxes")) {

					/*Money System*/
					//return some money to  the player, if the box has turret on it.
					if(hit.transform.childCount > 0){
						GetComponent<PlaceTurret>().money += 40;
					}

					Destroy(hit.transform.gameObject);
					boxCount++;
					boxText.text = "Boxes: " + boxCount;


				}
			}
			else {
				if (boxCount > 0) {
					boxPos = new Vector3(transform.position.x, 0.5f, transform.position.z) + (transform.forward * 1.1f);
					boxPos.x = Mathf.Round(boxPos.x);
					boxPos.z = Mathf.Round(boxPos.z);
					Instantiate(box, boxPos, new Quaternion());
					boxCount--;
					boxText.text = "Boxes: " + boxCount;


				}
			}
		}


		// if we are currently in a state called Locomotion (see line 25), then allow Jump input (Space) to set the Jump bool parameter in the Animator to true
		if (currentBaseState.nameHash == idleState) {
			Debug.Log ("in idle state");
			if (Input.GetButtonDown ("Jump")) {
				anim.SetBool ("Jump", true);
			}
		}
		
//		// if we are in the jumping state... 
//		else if (currentBaseState.nameHash == jumpState) {
//			Debug.Log("in jump state");
//			//  ..and not still in transition..
//			if (!anim.IsInTransition (0)) {
//				
//				if (useCurve)
//					// ..set the collider height to a float curve in the clip called ColliderHeight
//					col.height = anim.GetFloat ("ColliderHeight");
//				
//				// reset the Jump bool so we can jump again, and so that the state does not loop 
//				anim.SetBool ("Jump", false);
//			}
//		}


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
			var curSrc = src.Find(child.name);
			if (curSrc){
				CopyTransformRecurse (curSrc, child);
			}

		}

	}

	void OnCollisionEnter(Collision other) {
		currentObject = other.gameObject;
		if (other.gameObject.name != "Floor") {
			collision = true;
		}
	}

	void OnCollisionExit(Collision other) {
		print ("collision exited");
		collision = false;
	}
	IEnumerator Step() {
		yield return new WaitForSeconds (1);
	}

	IEnumerator WaitTwoSeconds() {
		yield return new WaitForSeconds(1);
		print (currentObject.name);
		Debug.Log (transform.forward);
		//transform.forward = new Vector3(0, 0, Mathf.Round (transform.forward.z));
		currentObject.transform.Translate (transform.forward * 1.0f);
	}

	void setHealthPoint(float num){
		healthPoint += num;
	}
}

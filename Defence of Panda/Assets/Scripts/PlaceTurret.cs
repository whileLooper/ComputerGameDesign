using UnityEngine;
using System.Collections;

public class PlaceTurret : MonoBehaviour {

	public GameObject turret1;
	public GameObject turret2;
	public GameObject turret3;
	private GameObject currentTurret;

	public float rayLength = 1.1f;
	private RaycastHit hit;
	private Vector3 turretPos;
	// Use this for initialization
	void Start () {
		currentTurret = turret2;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("t")) {
			if (Physics.Raycast(transform.position + new Vector3(0f, 0.5f, 0f), transform.forward, out hit, rayLength)) {
				print (hit.transform.gameObject.tag);
				if (hit.collider.gameObject.CompareTag("Boxes")) {
					turretPos = new Vector3(transform.position.x, 0.5f, transform.position.z) + (transform.forward * 1.1f);
					turretPos.x = Mathf.Round(turretPos.x);
					turretPos.z = Mathf.Round(turretPos.z);
					turretPos.y += 1.0f;
					Instantiate(currentTurret, turretPos, new Quaternion());

				}
			}
		}
	}
}

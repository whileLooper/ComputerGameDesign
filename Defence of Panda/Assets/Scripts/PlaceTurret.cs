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

	private GameObject turretClone;
	private GameObject hittedBox;

	// Use this for initialization
	void Start () {
		currentTurret = turret1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("t")) {

			if (Physics.Raycast(transform.position + new Vector3(0f, 0.5f, 0f), transform.forward, out hit, rayLength)) {
				print (hit.transform.gameObject.tag);
				if (hit.collider.gameObject.CompareTag("Boxes")) {
					hittedBox = hit.collider.gameObject;

					//if weapon not built yet, avoid building multiple weapons on top of one box
					if(hittedBox.transform.childCount == 0){
						turretPos = new Vector3(transform.position.x, 0.5f, transform.position.z) + (transform.forward * 1.1f);
						turretPos.x = Mathf.Round(turretPos.x);
						turretPos.z = Mathf.Round(turretPos.z);
						turretPos.y += 1.0f;

						//instantiate turret as child object of the corresponding (ray hitted) box
						turretClone = Instantiate(currentTurret, turretPos, new Quaternion()) as GameObject;
						turretClone.transform.parent = hittedBox.transform;
					}

				}
			}


		}
	}
}

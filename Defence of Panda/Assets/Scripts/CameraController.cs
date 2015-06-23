// PandaDev
// Shen Yang, Bo Chen, Ryan Diaz, Yuanzheng Zhu

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public float smooth = 3f;		// a public variable to adjust smoothing of camera motion
	//Transform standardPos;			// the usual position for the camera, specified by a transform in the game
	//Transform lookAtPos;			// the position to move the camera to when using head look

	Transform targetPos;
	Vector3 offset;

	void Start()
	{
		// initialising references
		//standardPos = GameObject.Find ("CamPos").transform;
		transform.position = GameObject.Find ("CamPos").transform.position;
		targetPos = GameObject.Find ("Player").transform;
		offset = targetPos.position - transform.position;
	}
	
	void FixedUpdate ()
	{

		// return the camera to standard position and direction
		//standardPos.position = new Vector3 (standardPos.position.x, standardPos.position.y + cameraHeight, 
		                                    //standardPos.position.z + cameraDistance);
		//transform.position = Vector3.Lerp(transform.position, new Vector3 (standardPos.position.x, standardPos.position.y + cameraHeight, 
		                                                                   //standardPos.position.z + cameraDistance), Time.deltaTime * smooth);	
		//transform.forward = Vector3.Lerp(transform.forward, standardPos.forward, Time.deltaTime * smooth);

		Vector3 targetCameraPos = targetPos.position - offset;
		transform.position = Vector3.Lerp (transform.position, targetCameraPos, Time.deltaTime * smooth);
	}
}

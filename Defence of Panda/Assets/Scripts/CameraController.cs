using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public float smooth = 3f;		// a public variable to adjust smoothing of camera motion
	Transform standardPos;			// the usual position for the camera, specified by a transform in the game
	Transform lookAtPos;			// the position to move the camera to when using head look
	
	void Start()
	{
		// initialising references
		standardPos = GameObject.Find ("CamPos").transform;

	}
	
	void FixedUpdate ()
	{

			// return the camera to standard position and direction
			transform.position = Vector3.Lerp(transform.position, standardPos.position, Time.deltaTime * smooth);	
			transform.forward = Vector3.Lerp(transform.forward, standardPos.forward, Time.deltaTime * smooth);
		
	}
}

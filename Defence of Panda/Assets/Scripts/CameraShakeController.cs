//yzhu319, 07/03/2015
using UnityEngine;
using System.Collections;

public class CameraShakeController : MonoBehaviour {

	GameObject earthquake;
	ParticleSystem earthquakeParticle;


	// Grabs the camera's transform
	public Transform camTransform;
	public float shakeDuration = 4.5f;
	
	// Amplitude of the shake.
	public float shakeAmount = 0.2f;
	public float decreaseFactor = 1.0f;
	
	Vector3 originalPos;


	void Start () {



		earthquake = GameObject.FindGameObjectWithTag("Earthquake");
		earthquakeParticle = earthquake.GetComponent<ParticleSystem>();


		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}


	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Q)){
			earthquakeParticle.Play();
			Shake();
		}
	}

	void Shake(){
		if(ShakeCamera() != null){
			StopCoroutine(ShakeCamera());
		}
		StartCoroutine(ShakeCamera());
	}

	IEnumerator ShakeCamera(){
		while (shakeDuration > 0)
		{
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			shakeDuration -= Time.deltaTime * decreaseFactor;
			Debug.Log(camTransform.localPosition);
			Debug.Log(shakeDuration);
			yield return camTransform.localPosition;
		}
		Debug.Log(shakeDuration);

		shakeDuration = 4.5f;
		camTransform.localPosition = originalPos;


	}
}

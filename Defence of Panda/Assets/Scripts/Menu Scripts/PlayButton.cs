using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void easy() {
		Application.LoadLevel ("AlphaDemo");
		PlayerScript.boxCount = 10;
		PlaceTurret.money = 400;
	}

	public void medium() {
		Application.LoadLevel ("AlphaDemo");
		PlayerScript.boxCount = 7;
		PlaceTurret.money = 200;
	}

	public void hard() {
		Application.LoadLevel ("AlphaDemo");
		PlayerScript.boxCount = 5;
		PlaceTurret.money = 100;
	}
}

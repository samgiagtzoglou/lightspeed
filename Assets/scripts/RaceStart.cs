using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RaceStart : MonoBehaviour {

	public Text timerText;
	private int secondsLeft;
	public Transform karts;

	// Use this for initialization
	void Start () {
		karts = (Transform) GameObject.Find ("allCarts").transform;
		InvokeRepeating ("startRace", 0.1f, 1);
		secondsLeft = 4;
	}


	void startRace() {
		secondsLeft--;
		if (secondsLeft > 0) {
			timerText.text = "" + secondsLeft;
		} else if (secondsLeft > -2) {
			timerText.text = "START!";
			allowDriving ();
		} else {
			timerText.text = "";
			CancelInvoke ();
		}
	}

	void allowDriving() {
		foreach (Transform kart in karts) {
			CarController kartScript = kart.GetComponent<CarController> ();
			kartScript.startDriving ();
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}

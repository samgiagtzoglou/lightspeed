using UnityEngine;
using System.Collections;

public class RaceStart : MonoBehaviour {

	public TextMesh timerText;
	private int secondsPassed;
	public Transform karts;


	// Use this for initialization
	void Start () {
		
		InvokeRepeating ("startRace", 0.1f, 1);
	}

	void startRace() {
		secondsPassed++;
		if (secondsPassed < 4) {
			timerText.text = "" + secondsPassed;
		} else {
			timerText.text = "START!";
			allowDriving ();
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

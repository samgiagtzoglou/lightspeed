using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RaceStart : MonoBehaviour {

	public Text timerText;
	private int secondsLeft;
	public Transform karts;

	// Use this for initialization
	void Start () {
		timerText.text = "";
		karts = (Transform) GameObject.Find ("allCarts").transform;
		InvokeRepeating ("startRace", 0.1f, 1);
		secondsLeft = 20;
		GameObject.Find("Cameras").transform.FindChild("IntroCamera").gameObject.SetActive(true);
	}


	void startRace() {
		Configurator conf = GameObject.Find("Configurator").GetComponent<Configurator>();

		secondsLeft--;
		if (secondsLeft < 4) {
			
			switch( SceneConfig.players ) {
				case 1:
					conf.camera1p.gameObject.SetActive (true);
					break;
				case 2:
					conf.camera12p.gameObject.SetActive (true);
					conf.camera22p.gameObject.SetActive (true);
					break;

				case 3:
					conf.camera13p.gameObject.SetActive (true);
					conf.camera23p.gameObject.SetActive (true);
					conf.camera33p.gameObject.SetActive (true);
					break;
				case 4:
					conf.camera14p.gameObject.SetActive (true);
					conf.camera24p.gameObject.SetActive (true);
					conf.camera34p.gameObject.SetActive (true);
					conf.camera44p.gameObject.SetActive (true);
					break;
			}
			GameObject.Find("Cameras").transform.FindChild("IntroCamera").gameObject.SetActive(false);
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

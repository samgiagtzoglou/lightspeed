using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCanvas : MonoBehaviour {
	PositionAndLapManager plm;
	int player;
	bool finished = false;
	// Use this for initialization
	void Start () {
		plm = GameObject.Find("raceManager").GetComponent<PositionAndLapManager>();
		player = (int)char.GetNumericValue(this.name[1]);
	}
	
	// Update is called once per frame
	void Update () {
		finished = (PolePosition.finishOrder[player] > 0);

		if (finished) {
			this.transform.FindChild ("positionText").GetComponent<Text> ().text = "";
			this.transform.FindChild ("lapText").GetComponent<Text> ().text = "";
			GameObject places = GameObject.Find ("p" + player.ToString() + "Canvas").transform.FindChild ("Places").gameObject;
			for (int j = 1; j <= SceneConfig.players; j++) {
				places.transform.FindChild (j.ToString()).gameObject.SetActive(true);
				if (PolePosition.finishOrder[j] > 0) {
					int place = PolePosition.finishOrder [j];
					Text placeText = places.transform.FindChild (place.ToString()).gameObject.GetComponent<Text> ();
					placeText.text = place + ": Player " + (j);
				}
			}
		} else {
			if (plm.cartPositions.ContainsKey ("player" + player.ToString ())) {
				int position = plm.cartPositions ["player" + player.ToString ()] + 1;
				this.transform.FindChild ("positionText").GetComponent<Text> ().text = "Place " + position.ToString ()+ "/" + plm.numPlayers.ToString();
			}
			if (plm.cartLap.ContainsKey("player"+player.ToString())){
				int lap = plm.cartLap ["player" + player.ToString ()] + 1;
				this.transform.FindChild ("lapText").GetComponent<Text> ().text = "Lap " + lap.ToString () + "/" + plm.numLaps.ToString();
			}
		}
	}
}

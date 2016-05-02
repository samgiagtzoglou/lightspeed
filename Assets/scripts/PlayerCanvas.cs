using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCanvas : MonoBehaviour {
	PositionAndLapManager plm;
	int player;
	// Use this for initialization
	void Start () {
		plm = GameObject.Find("raceManager").GetComponent<PositionAndLapManager>();
		player = (int)char.GetNumericValue(this.name[1]);
		Debug.Log (player);
	}
	
	// Update is called once per frame
	void Update () {
		if (plm.cartPositions.ContainsKey ("player" + player.ToString ())) {
			int position = plm.cartPositions ["player" + player.ToString ()];
			this.transform.FindChild ("positionText").GetComponent<Text> ().text = position.ToString ();
		}
		if (plm.cartLap.ContainsKey("player"+player.ToString())){
			int lap = plm.cartLap ["player"+player.ToString()];

			this.transform.FindChild ("positionText").GetComponent<Text> ().text = player.ToString();
		}
	}
}

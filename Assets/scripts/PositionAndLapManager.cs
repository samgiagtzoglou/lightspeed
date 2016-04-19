using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PositionAndLapManager : MonoBehaviour {
	private int numPlayers;
	private CartPosition[] allCarts;
	public CartPosition[] carOrder;
	public Dictionary<string,int> cartPositions;
	public int numLaps = 1;

	public void Start() {
		numPlayers = SceneConfig.players;
		// set up the car objects
		carOrder = new CartPosition[numPlayers];
		cartPositions = new Dictionary<string, int>();
		allCarts = new CartPosition[numPlayers];
		Configurator config = (Configurator) GameObject.Find ("Configurator").GetComponent("Configurator");
		for (int i = 1; i <= numPlayers; i++) {
			string curPlayerName = "player" + i;
			GameObject curPlayerObject = GameObject.FindWithTag (curPlayerName);
			Debug.Log (curPlayerObject);
			allCarts[i-1] = (CartPosition) curPlayerObject.GetComponent<CartPosition>();
		}
		foreach (CartPosition pos in allCarts) {
			cartPositions.Add (pos.tag, 1);
		}
	}

	// this gets called every frame
	public void Update() {
		foreach (CartPosition pos in allCarts) {
			Debug.Log (pos);
			Debug.Log (pos.lastWaypoint);
			if (pos.lastWaypoint == null) {
				return;
			}
			int val = pos.GetCarPosition (allCarts);
			cartPositions [pos.tag] = val - 1;
			Debug.Log (pos.tag);
			CarController cartController = (CarController) pos.GetComponent("CarController");
			cartController.position = val;
			if (pos.currentLap == numLaps + 1) {
				Debug.Log (pos.tag + " is the Winner!");
			}
			if (pos.tag == "kart1") {
				GameObject posTextObj = GameObject.Find ("position");
				Text posText = posTextObj.GetComponent<Text> ();
				posText.text = "Position: " + val;
			}
		}
	}

	public void setNumPlayers(int numberPlayers) {
		numPlayers = numberPlayers;
	}
}

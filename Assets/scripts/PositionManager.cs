using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PositionManager : MonoBehaviour {
	private int numPlayers;
	private CartPosition[] allCarts;
	public CartPosition[] carOrder;
	public Dictionary<string,int> cartPositions;

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
			allCarts[i-1] = (CartPosition) curPlayerObject.GetComponent<CartPosition>();
		}
		foreach (CartPosition pos in allCarts) {
			cartPositions.Add (pos.name, 1);
		}
	}

	// this gets called every frame
	public void Update() {
		foreach (CartPosition pos in allCarts) {
			int val = pos.GetCarPosition (allCarts);
			cartPositions [pos.name] = val - 1;
			CarController kartController = (CarController)
				GameObject.Find(pos.name).GetComponent(typeof(CarController));
			kartController.position = val;
			if (pos.name == "kart1") {
				GameObject posTextObj = GameObject.Find ("position");
				Text posText = posTextObj.GetComponent<Text> ();
				posText.text = "Position: " + val;
			}
		}

	}
}

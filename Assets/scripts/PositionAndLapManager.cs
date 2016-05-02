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
	public Text timerText;
	public bool isPause = false;

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
//		timerText = GameObject.Find("timerText").text;
	}

	// this gets called every frame
	public void Update() {
		
		if( Input.GetButtonDown("C1_Start") || Input.GetButtonDown("C2_Start") || 
			Input.GetButtonDown("C3_Start") || Input.GetButtonDown("C4_Start") ||
			Input.GetButtonDown("Pause")){
			isPause = !isPause;
				if(isPause){
					GameObject.Find ("raceManager").transform.FindChild ("pauseText").gameObject.SetActive(true);
					Time.timeScale = 0;
				} else {
					GameObject.Find ("raceManager").transform.FindChild ("pauseText").gameObject.SetActive(false);
					Time.timeScale = 1;
				}
		}
	
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
			if (pos.currentLap == numLaps) {
				timerText.text = pos.tag + "Wins!";
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

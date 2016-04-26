using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SceneConfig : MonoBehaviour {
//	public int score;

	public static string player1ControlCode;
	public static string player2ControlCode;
	public static string player3ControlCode;
	public static string player4ControlCode;
	public static int players;
	public static bool controllerControl;
	public static bool gameArmed;
	public static Dictionary<string, bool> controllersActive = new Dictionary<string, bool>{{"C1",false},{"C2",false},{"C3",false},{"C4",false},{"KArrow",false},{"KWasd",false}};

	void Start() {
		players = 0;
		gameArmed = false;
		GameObject.Find ("armedGameText").gameObject.GetComponent<Text>().enabled = false;


//		controllerControl = true;
	}

	void Awake()
	{
		DontDestroyOnLoad(this);
	}

	void Update() {
		if (SceneManager.GetActiveScene().name == "game"){
			return;
		}
		if (gameArmed) {
//			gameArmed = true;
			GameObject.Find ("armedGameText").gameObject.GetComponent<Text>().enabled = true;
		} 
		if (players > 0) {
			gameArmed = true;
		}
		if (players < 4) {
			if (Input.GetButton ("C1_Start") && !controllersActive ["C1"]) {
				controllersActive ["C1"] = true;
				players = players + 1;
				setPlayerInput ("C1", players);
			} else if (Input.GetButton ("C1_Start") && gameArmed) {
				LoadLevel ("game");
			}

			if (Input.GetButton ("C2_Start") && !controllersActive ["C2"]) {
				controllersActive ["C2"] = true;
				players = players + 1;
				setPlayerInput ("C2", players);
			} else if (Input.GetButton ("C2_Start") && gameArmed) {
				LoadLevel ("game");
			}

			if (Input.GetButton ("C3_Start") && !controllersActive ["C3"]) {
				controllersActive ["C3"] = true;
				players = players + 1;
				setPlayerInput ("C3", players);
			} else if (Input.GetButton ("C3_Start") && gameArmed) {
				LoadLevel ("game");
			}

			if (Input.GetButton ("C4_Start") && !controllersActive ["C4"]) {
				controllersActive ["C4"] = true;
				players = players + 1;
				setPlayerInput ("C4", players);
			} else if (Input.GetButton ("C4_Start") && gameArmed) {
				LoadLevel ("game");
			}

			if (Input.GetButton ("KArrow_Start") && !controllersActive ["KArrow"]) {
				controllersActive ["KArrow"] = true;
				players = players + 1;
				setPlayerInput ("KArrow", players);
			} else if (Input.GetButton ("KArrow_Start") && gameArmed) {
				LoadLevel ("game");
			}

			if (Input.GetButton ("KWasd_Start") && !controllersActive ["KWasd"]) {
				controllersActive ["KWasd"] = true;
				players = players + 1;
				setPlayerInput ("KWasd", players);
			} else if (Input.GetButton ("KWasd_Start") && gameArmed) {
				LoadLevel ("game");
			}

		}
	}
	public void setPlayerInput(string controlCode, int player) {
		switch (player) {
		case 1:
//			GameObject.Find ("Player1Text").gameObject.GetComponent<Text>().material.color = None;
			GameObject.Find ("Player1Text").gameObject.GetComponent<Text>().color = Color.yellow;
			player1ControlCode = controlCode;
			break;
		case 2:
			GameObject.Find ("Player2Text").gameObject.GetComponent<Text>().color = Color.yellow;
			player2ControlCode = controlCode;
			break;
		case 3:
			GameObject.Find ("Player3Text").gameObject.GetComponent<Text>().color = Color.yellow;
			player3ControlCode = controlCode;
			break;
		case 4:
			GameObject.Find ("Player4Text").gameObject.GetComponent<Text>().color = Color.yellow;
			player4ControlCode = controlCode;
			break;
		}
	}

	public void setPlayers (Button button){

		if (button.name == "Button 1") {
			players = 1;
		}

		if (button.name == "Button 2") {
			players = 2;
		}

		if (button.name == "Button 3") {
			players = 3;
		}

		if (button.name == "Button 4") {
			players = 4;
		}

		controllerControl = true;
	}

	public void LoadLevel(string level){
		Debug.Log (players);
		Application.LoadLevel(level);


	}
}
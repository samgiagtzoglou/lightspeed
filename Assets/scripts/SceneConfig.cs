using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneConfig : MonoBehaviour {
	public GameObject car;
	public static int score;

	public static int players;
	public static bool controllerControl;

	void Start() {
		players = 1;
		controllerControl = true;
	}

	void Awake()
	{
		DontDestroyOnLoad(this);
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

		controllerControl = false;
	}

	public void LoadLevel(string level){
		Debug.Log (players);
		Application.LoadLevel(level);


	}
}
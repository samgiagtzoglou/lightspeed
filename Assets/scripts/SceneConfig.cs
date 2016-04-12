using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneConfig : MonoBehaviour {
	public GameObject car;
	public static int score;

	public static int players;

	void Start() {

	}

	void Awake()
	{
		DontDestroyOnLoad(this);
	}

	public void LoadLevel(string level){
		Debug.Log (players);
		Application.LoadLevel(level);


	}

	public void setPlayers (Slider slider){
		players = (int) slider.value;
	}
}
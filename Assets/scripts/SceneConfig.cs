using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SceneConfig : MonoBehaviour {
//	public int score;

	public static string player1ControlCode;
	public static string player2ControlCode;
	public static string player3ControlCode;
	public static string player4ControlCode;
	public static int players;
	public static bool controllerControl;
	public static bool gameArmed;
	public static Dictionary<string, int> controllersActive = new Dictionary<string, int>{{"C1",0},{"C2",0},{"C3",0},{"C4",0},{"KArrow",0},{"KWasd",0}};
	public AudioClip alert;
	AudioSource audio;
	public AudioClip alert1;
	AudioSource audio1;
	public AudioClip alert2;
	AudioSource audio2;
	public AudioClip alert3;
	AudioSource audio3;

	private bool xaxis1inuse;
	private bool xaxis2inuse;
	private bool xaxis3inuse;
	private bool xaxis4inuse;
	private bool xaxisWasdinuse;
	private bool xaxisArrowinuse;

	public static Dictionary<int, bool> playersReady = new Dictionary<int, bool>{{1,false},{2,true},{3,true},{4,true}};
	public static Dictionary<int, int> playersColors = new Dictionary<int, int>{{1,0},{2,0},{3,0},{4,0}};


	void Start() {
		players = 0;
		gameArmed = false;
		GameObject.Find ("armedGameText").gameObject.GetComponent<Text>().enabled = false;
		GameObject.Find ("pressToJoinText").gameObject.GetComponent<Text>().enabled = true;
		GameObject.Find ("buttonSet2").transform.GetChild (0).gameObject.SetActive (false);
		GameObject.Find ("buttonSet1").transform.GetChild (0).gameObject.SetActive (true);
		GameObject.Find ("buttonSet1").transform.GetChild (1).gameObject.SetActive (true);
		audio = GetComponent<AudioSource> ();
		audio1 = GetComponent<AudioSource> ();
		audio2 = GetComponent<AudioSource> ();
		audio3 = GetComponent<AudioSource> ();


		//GameObject.Find ("MenuScreen").GetComponent<AudioSource>().PlayOneShot(alert);
	}

	void Awake()
	{
		DontDestroyOnLoad(this);
	}

	void Update() {
		if (SceneManager.GetActiveScene().name == "game"){
			return;
		}
		if (playersReady [1] && playersReady [2] && playersReady [3] && playersReady [4]) {
			GameObject.Find ("armedGameText").gameObject.GetComponent<Text> ().enabled = true;
			GameObject.Find ("pressToJoinText").gameObject.GetComponent<Text>().enabled = false;
			GameObject.Find ("buttonSet2").transform.GetChild (0).gameObject.SetActive (true);
			GameObject.Find ("buttonSet1").transform.GetChild (0).gameObject.SetActive (false);
			GameObject.Find ("buttonSet1").transform.GetChild (1).gameObject.SetActive (false);

			gameArmed = true;
		} else {
			GameObject.Find ("armedGameText").gameObject.GetComponent<Text> ().enabled = false;
			GameObject.Find ("pressToJoinText").gameObject.GetComponent<Text>().enabled = true;
			GameObject.Find ("buttonSet2").transform.GetChild (0).gameObject.SetActive (false);

			GameObject.Find ("buttonSet1").transform.GetChild (0).gameObject.SetActive (true);
			GameObject.Find ("buttonSet1").transform.GetChild (1).gameObject.SetActive (true);
		}
		if (players > 0) {
			if (Mathf.Abs(Input.GetAxis ("C1_X axis"))>0.5 && (controllersActive ["C1"] > 0) && !xaxis1inuse) {
				xaxis1inuse = true;
				bool right = Input.GetAxis ("C1_X axis") > 0 ? true : false;
				moveColorSelection (controllersActive ["C1"], right);
			} else if (Mathf.Abs(Input.GetAxis ("C1_X axis"))<0.1 && (controllersActive ["C1"] > 0) && xaxis1inuse){
				xaxis1inuse = false;
			}

			if (Mathf.Abs(Input.GetAxis ("C2_X axis"))>0.5 && (controllersActive ["C2"] > 0) && !xaxis2inuse) {
				xaxis2inuse = true;
				bool right = Input.GetAxis ("C2_X axis") > 0 ? true : false;
				moveColorSelection (controllersActive ["C2"], right);
			} else if (Mathf.Abs(Input.GetAxis ("C2_X axis"))<0.1 && (controllersActive ["C2"] > 0) && xaxis2inuse){
				xaxis2inuse = false;
			}

			if (Mathf.Abs(Input.GetAxis ("C3_X axis"))>0.5 && (controllersActive ["C3"] > 0) && !xaxis3inuse) {
				xaxis3inuse = true;
				bool right = Input.GetAxis ("C3_X axis") > 0 ? true : false;
				moveColorSelection (controllersActive ["C3"], right);
			} else if (Mathf.Abs(Input.GetAxis ("C3_X axis"))<0.1 && (controllersActive ["C3"] > 0) && xaxis3inuse){
				xaxis3inuse = false;
			}

			if (Mathf.Abs(Input.GetAxis ("C4_X axis"))>0.5 && (controllersActive ["C4"] > 0) && !xaxis4inuse) {
				xaxis4inuse = true;
				bool right = Input.GetAxis ("C4_X axis") > 0 ? true : false;
				moveColorSelection (controllersActive ["C4"], right);
			} else if (Mathf.Abs(Input.GetAxis ("C4_X axis"))<0.1 && (controllersActive ["C4"] > 0) && xaxis4inuse){
				xaxis4inuse = false;
			}

			if (Mathf.Abs(Input.GetAxis ("KArrow_X axis"))>0.1 && (controllersActive ["KArrow"] > 0) && !xaxisArrowinuse) {
				xaxisArrowinuse = true;
				bool right = Input.GetAxis ("KArrow_X axis") > 0 ? true : false;
				moveColorSelection (controllersActive ["KArrow"], right);
			} else if (Mathf.Abs(Input.GetAxis ("KArrow_X axis"))<0.1 && (controllersActive ["KArrow"] > 0) && xaxisArrowinuse){
				xaxisArrowinuse = false;
			}

			if (Mathf.Abs(Input.GetAxis ("KWasd_X axis"))>0.1 && (controllersActive ["KWasd"] > 0) && !xaxisWasdinuse) {
				xaxisWasdinuse = true;
				bool right = Input.GetAxis ("KWasd_X axis") > 0 ? true : false;
				moveColorSelection (controllersActive ["KWasd"], right);
			} else if (Mathf.Abs(Input.GetAxis ("KWasd_X axis"))<0.1 && (controllersActive ["KWasd"] > 0) && xaxisWasdinuse){
				xaxisWasdinuse = false;
			}

			if (Input.GetButtonDown ("C1_Fire") && (controllersActive ["C1"] > 0) && !playersReady[controllersActive ["C1"]]) {
				Debug.Log ("C1 ready");
				setPlayerReady (controllersActive ["C1"]);

			}
			if (Input.GetButtonDown ("C2_Fire") && (controllersActive ["C2"] > 0) && !playersReady[controllersActive ["C2"]])  {
				setPlayerReady (controllersActive ["C2"]);
			} 
			if (Input.GetButtonDown ("C3_Fire") && (controllersActive ["C3"] > 0) && !playersReady[controllersActive ["C3"]])  {
				setPlayerReady (controllersActive ["C3"]);
			} 
			if (Input.GetButtonDown ("C4_Fire") && (controllersActive ["C4"] > 0) && !playersReady[controllersActive ["C14"]])  {
				setPlayerReady (controllersActive ["C4"]);
			} 
			if (Input.GetButtonDown ("KArrow_Start") && (controllersActive ["KArrow"] > 0) && !playersReady[controllersActive ["KArrow"]]){
				setPlayerReady (controllersActive ["KArrow"]);
			} 
			if (Input.GetButtonDown ("KWasd_Start") && (controllersActive ["KWasd"] > 0) && !playersReady[controllersActive ["KWasd"]]){
				setPlayerReady (controllersActive ["KWasd"]);
			} 
		}
		if (players < 4) {
			if (Input.GetButton ("C1_Start") && controllersActive ["C1"]==0) {
				playersReady [players + 1] = false;
				controllersActive ["C1"] = players + 1;
				players = players + 1;
				setPlayerInput ("C1", players);
			} else if (Input.GetButton ("C1_Start") && gameArmed) {
				LoadLevel ("game");
			}

			if (Input.GetButton ("C2_Start") && controllersActive ["C2"]==0) {
				playersReady [players + 1] = false;
				controllersActive ["C2"] = players + 1;
				players = players + 1;
				setPlayerInput ("C2", players);
			} else if (Input.GetButton ("C2_Start") && gameArmed) {
				LoadLevel ("game");
			}

			if (Input.GetButton ("C3_Start") && controllersActive ["C3"]==0) {
				playersReady [players + 1] = false;
				controllersActive ["C3"] = players + 1;
				players = players + 1;
				setPlayerInput ("C3", players);
			} else if (Input.GetButton ("C3_Start") && gameArmed) {
				LoadLevel ("game");
			}

			if (Input.GetButton ("C4_Start") && controllersActive ["C4"]==0) {
				playersReady [players + 1] = false;
				controllersActive ["C4"] = players + 1;
				players = players + 1;
				setPlayerInput ("C4", players);
			} else if (Input.GetButton ("C4_Start") && gameArmed) {
				LoadLevel ("game");
			}

			if (Input.GetButtonDown ("KArrow_Start") && controllersActive ["KArrow"]==0) {
				playersReady [players + 1] = false;
				controllersActive ["KArrow"] = players + 1;
				players = players + 1;
				setPlayerInput ("KArrow", players);
			} else if (Input.GetButton ("KArrow_Start") && gameArmed) {
				LoadLevel ("game");
			}

			if (Input.GetButtonDown ("KWasd_Start") && controllersActive ["KWasd"]==0) {
				playersReady [players + 1] = false;
				controllersActive ["KWasd"] = players + 1;
				players = players + 1;
				setPlayerInput ("KWasd", players);
			} else if (Input.GetButton ("KWasd_Start") && gameArmed) {
				LoadLevel ("game");
			}

		}
	}
	public void setPlayerInput(string controlCode, int player) {
		Debug.Log ("Called setPlayerInput");
		switch (player) {
		case 1:
			Debug.Log ("Setting P1");
//			GameObject.Find ("Player1Text").gameObject.GetComponent<Text>().material.color = None;
			GameObject.Find("JackCanvas").transform.FindChild("P1Box").gameObject.SetActive(true);
//			GameObject.Find("P1Box").gameObject.activeInHierarchy = true;
			player1ControlCode = controlCode;
			audio.PlayOneShot (alert, 1);
			break;
		case 2:
			Debug.Log ("Setting P2");
			GameObject.Find("JackCanvas").transform.FindChild("P2Box").gameObject.SetActive(true);
			player2ControlCode = controlCode;
			audio.PlayOneShot (alert, 1);
			break;
		case 3:
			Debug.Log ("Setting P3");
			GameObject.Find("JackCanvas").transform.FindChild("P3Box").gameObject.SetActive(true);
			player3ControlCode = controlCode;
			audio.PlayOneShot (alert, 1);
			break;
		case 4:
			Debug.Log ("Setting P4");
			GameObject.Find("JackCanvas").transform.FindChild("P4Box").gameObject.SetActive(true);
			player4ControlCode = controlCode;
			audio.PlayOneShot (alert, 1);
			break;
		}
	}
	private void moveColorSelection(int player, bool right){
		Toggle old = GameObject.Find(player+"ColorToggles").gameObject.GetComponent<ToggleGroup>().ActiveToggles().FirstOrDefault();
		Toggle newToggle;
		if (right) {
			newToggle = (Toggle) old.FindSelectableOnRight ();

			if (newToggle) {
				old.isOn = false;
				newToggle.gameObject.GetComponent<Toggle> ().isOn = true;
				GameObject playerObject = GameObject.Find("P"+player+"Box").transform.FindChild("carBeta").gameObject;
				setColorForCart (playerObject, getLightwaveForColor (newToggle.name));
				audio.PlayOneShot (alert1, 1);
			}


		} else  {
			newToggle = (Toggle) old.FindSelectableOnLeft ();
			if (newToggle) {
				old.isOn = false;
				newToggle.gameObject.GetComponent<Toggle> ().isOn = true;
				GameObject playerObject = GameObject.Find("P"+player+"Box").transform.FindChild("carBeta").gameObject;
				setColorForCart (playerObject, getLightwaveForColor (newToggle.name));
				audio.PlayOneShot (alert1, 1);
			}
		}


	}
	private void setPlayerReady(int player){
		Toggle color = GameObject.Find(player+"ColorToggles").gameObject.GetComponent<ToggleGroup>().ActiveToggles().FirstOrDefault();
		playersColors [player] = getLightwaveForColor(color.name);
		playersReady [player] = true;
			Debug.Log ("Ready P"+player);
		GameObject.Find("P"+player+"Box").transform.FindChild("Ready").gameObject.SetActive(true);
		GameObject.Find("P"+player+"Box").transform.FindChild(player+"ColorToggles").gameObject.SetActive(false);
		audio.PlayOneShot (alert2, 1);
	}
	private int getLightwaveForColor(string color){
		Debug.Log ("retrieving color " + color);
		switch (color) {
		case "Black":
			return 0;
		case "Purple":
			return 400;
		case "Orange":
			return 600;
		case "Green":
			return 530;
		case "Red":
			return 700;
		case "Blue":
			return 450;
		case "Teal":
			return 490;
		}

		return 0;
	
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
		audio.PlayOneShot (alert3, 1);
		Application.LoadLevel(level);
	

	}
	void setColorForCart(GameObject player, int color) {
		CarController playerController = (CarController) player.GetComponent(typeof(CarController));
		playerController.wavelength = color;
		Renderer cartRenderer = (Renderer) player.transform.Find("Meshes/cart").GetComponent<Renderer> ();
		cartRenderer.material.SetInt ("_Wavelength", color);
		Renderer tailRenderer = (Renderer) player.transform.Find("Meshes/tail").GetComponent<Renderer> ();
		tailRenderer.material.SetInt ("_Wavelength", color);
	}
}

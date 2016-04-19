using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Configurator : MonoBehaviour {
	public GameObject car;
	public GameObject camera1p;
	public GameObject camera12p;
	public GameObject camera22p;
	public GameObject camera13p;
	public GameObject camera23p;
	public GameObject camera33p;
	public GameObject camera14p;
	public GameObject camera24p;
	public GameObject camera34p;
	public GameObject camera44p;

	//The Carts
	public GameObject allCarts;
	public GameObject player1;
	public GameObject player2;
	public GameObject player3;
	public GameObject player4;

	//The race manager
	public GameObject raceManagerPrefab;
	public GameObject raceManager;

	void Start() {
		allCarts = GameObject.Find ("allCarts");
		Vector3 position1 = GameObject.Find ("firstStart").transform.position;
		Vector3 position2 = GameObject.Find ("secondStart").transform.position;
		Vector3 position3 = GameObject.Find ("thirdStart").transform.position;
		Vector3 position4 = GameObject.Find ("fourthStart").transform.position;

		if (SceneConfig.players == 0) {
			SceneConfig.players = 1;
		}
		if (SceneConfig.players == 1) {
			camera1p.gameObject.SetActive (true);
			player1 = (GameObject) Instantiate(car, position1, Quaternion.identity);
			player1.transform.parent = allCarts.transform;

			CarController controller = (CarController) player1.GetComponent ("CarController");
			if (SceneConfig.controllerControl) {
				
				controller.xaxis = "1_X axis";
				controller.yaxis = "1_RT";
				controller.brakeaxis = "1_LT";
			} else {
				
				controller.xaxis = "ArrowKeyboard_X axis";
				controller.yaxis = "ArrowKeyboard_RT";
			}

			TrackObject script = (TrackObject) camera1p.GetComponent("TrackObject");
			player1.tag = "player1";
			script.target = player1.transform;

		} else if (SceneConfig.players == 2) {
				
			camera12p.gameObject.SetActive (true);
			camera22p.gameObject.SetActive (true);

			player1 = (GameObject) Instantiate(car, position1, Quaternion.identity);
			player2 = (GameObject) Instantiate(car, position2, Quaternion.identity);

			CarController controller1 = (CarController) player1.GetComponent ("CarController");
			CarController controller2 = (CarController) player2.GetComponent ("CarController");

			if (SceneConfig.controllerControl) {
				controller1.xaxis = "1_X axis";
				controller1.yaxis = "1_RT";
				controller1.brakeaxis = "1_LT";
				controller2.xaxis = "2_X axis";
				controller2.yaxis = "2_RT";
				controller2.brakeaxis = "2_LT";
			} else {
				controller1.xaxis = "ArrowKeyboard_X axis";
				controller1.yaxis = "ArrowKeyboard_RT";
				controller2.xaxis = "WasdKeyboard_X axis";
				controller2.yaxis = "WasdKeyboard_RT";
			}

			TrackObject script1 = (TrackObject) camera12p.GetComponent("TrackObject");
			TrackObject script2 = (TrackObject) camera22p.GetComponent("TrackObject");

			player1.transform.parent = allCarts.transform;
			player2.transform.parent = allCarts.transform;

			player1.tag = "player1";
			player2.tag = "player2";

			script1.target = player1.transform;
			script2.target = player2.transform;
		} else if (SceneConfig.players == 3) {
			camera13p.gameObject.SetActive (true);
			camera23p.gameObject.SetActive (true);
			camera33p.gameObject.SetActive (true);

			player1 = (GameObject) Instantiate(car, position1, Quaternion.identity);
			player2 = (GameObject) Instantiate(car, position2, Quaternion.identity);
			player3 = (GameObject) Instantiate(car, position3, Quaternion.identity);

			CarController controller1 = (CarController) player1.GetComponent ("CarController");
			controller1.xaxis = "1_X axis";
			controller1.yaxis = "1_RT";

			CarController controller2 = (CarController) player2.GetComponent ("CarController");
			controller2.xaxis = "2_X axis";
			controller2.yaxis = "2_RT";

			CarController controller3 = (CarController) player3.GetComponent ("CarController");
			controller3.xaxis = "3_X axis";
			controller3.yaxis = "3_RT";


			TrackObject script1 = (TrackObject) camera13p.GetComponent("TrackObject");
			TrackObject script2 = (TrackObject) camera23p.GetComponent("TrackObject");
			TrackObject script3 = (TrackObject) camera33p.GetComponent("TrackObject");

			player1.transform.parent = allCarts.transform;
			player2.transform.parent = allCarts.transform;
			player3.transform.parent = allCarts.transform;

			player1.tag = "player1";
			player2.tag = "player2";
			player3.tag = "player3";

			script1.target = player1.transform;
			script2.target = player2.transform;
			script3.target = player3.transform;

		} else if (SceneConfig.players == 4) {
			camera14p.gameObject.SetActive (true);
			camera24p.gameObject.SetActive (true);
			camera34p.gameObject.SetActive (true);
			camera44p.gameObject.SetActive (true);

			player1 = (GameObject) Instantiate(car, position1, Quaternion.identity);
			player2 = (GameObject) Instantiate(car, position2, Quaternion.identity);
			player3 = (GameObject) Instantiate(car, position3, Quaternion.identity);
			player4 = (GameObject) Instantiate(car, position4, Quaternion.identity);

			CarController controller1 = (CarController) player1.GetComponent ("CarController");
			controller1.xaxis = "1_X axis";
			controller1.yaxis = "1_RT";

			CarController controller2 = (CarController) player2.GetComponent ("CarController");
			controller2.xaxis = "2_X axis";
			controller2.yaxis = "2_RT";

			CarController controller3 = (CarController) player3.GetComponent ("CarController");
			controller3.xaxis = "3_X axis";
			controller3.yaxis = "3_RT";

			CarController controller4 = (CarController) player4.GetComponent ("CarController");
			controller4.xaxis = "4_X axis";
			controller4.yaxis = "4_RT";

			TrackObject script1 = (TrackObject) camera14p.GetComponent("TrackObject");
			TrackObject script2 = (TrackObject) camera24p.GetComponent("TrackObject");
			TrackObject script3 = (TrackObject) camera34p.GetComponent("TrackObject");
			TrackObject script4 = (TrackObject) camera44p.GetComponent("TrackObject");


			player1.transform.parent = allCarts.transform;
			player2.transform.parent = allCarts.transform;
			player3.transform.parent = allCarts.transform;
			player4.transform.parent = allCarts.transform;

			player1.tag = "player1";
			player2.tag = "player2";
			player3.tag = "player3";
			player4.tag = "player4";

			script1.target = player1.transform;
			script2.target = player2.transform;
			script3.target = player3.transform;
			script4.target = player4.transform;
		}
		raceManager = (GameObject)Instantiate (raceManagerPrefab);
		PositionAndLapManager lapsMgr = (PositionAndLapManager)raceManager.GetComponent ("PositionAndLapManager");
		lapsMgr.setNumPlayers (SceneConfig.players);

//		Debug.Log ("set num players to: " + SceneConfig.players);
	}

	void Awake() {
		DontDestroyOnLoad(this);
	}


		
}
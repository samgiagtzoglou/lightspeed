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
//	public GameObject raceManagerPrefab;
	public GameObject raceManager;

	public string finishTag = "finishLine";

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

		
			setColorForCart(player1, SceneConfig.playersColors[1]);

			CarController controller = (CarController) player1.GetComponent ("CarController");
			controller.xaxis = SceneConfig.player1ControlCode + "_X axis";
			controller.yaxis = SceneConfig.player1ControlCode + "_RT";
			if (SceneConfig.player1ControlCode.StartsWith ("C")) {
				controller.brakeaxis = SceneConfig.player1ControlCode + "_LT";
				controller.fireButton = SceneConfig.player1ControlCode + "_Fire";

			} else {
				controller.fireButton = SceneConfig.player1ControlCode + "_Start";
			}


			TrackObject script = (TrackObject) camera1p.GetComponent("TrackObject");
			player1.tag = "player1";
			script.target = player1.transform;

			Canvas c1 = (Canvas) GameObject.Find("PlayerCanvases").transform.FindChild("p1Canvas").GetComponent<Canvas> ();
			c1.worldCamera = (Camera) camera1p.GetComponent<Camera>();
			c1.gameObject.SetActive (true);
			c1.planeDistance = 1;

		} else if (SceneConfig.players == 2) {
				
			camera12p.SetActive (true);
			camera22p.SetActive (true);

			player1 = (GameObject) Instantiate(car, position1, Quaternion.identity);
			player2 = (GameObject) Instantiate(car, position2, Quaternion.identity);

			setColorForCart(player1, SceneConfig.playersColors[1]);
			setColorForCart(player2, SceneConfig.playersColors[2]);

			CarController controller1 = (CarController) player1.GetComponent ("CarController");
			CarController controller2 = (CarController) player2.GetComponent ("CarController");

			controller1.xaxis = SceneConfig.player1ControlCode + "_X axis";
			controller1.yaxis = SceneConfig.player1ControlCode + "_RT";

			if (SceneConfig.player1ControlCode.StartsWith ("C")) {
				controller1.brakeaxis = SceneConfig.player1ControlCode + "_LT";
				controller1.fireButton = SceneConfig.player1ControlCode + "_Fire";
			} else {
				controller1.fireButton = SceneConfig.player1ControlCode + "_Start";
			}

			controller2.xaxis = SceneConfig.player2ControlCode + "_X axis";
			controller2.yaxis = SceneConfig.player2ControlCode + "_RT";

			if (SceneConfig.player2ControlCode.StartsWith ("C")) {
				controller2.brakeaxis = SceneConfig.player2ControlCode + "_LT";
				controller2.fireButton = SceneConfig.player2ControlCode + "_Fire";
			} else {
				controller2.fireButton = SceneConfig.player2ControlCode + "_Start";
			}

			TrackObject script1 = (TrackObject) camera12p.GetComponent("TrackObject");
			TrackObject script2 = (TrackObject) camera22p.GetComponent("TrackObject");

			player1.transform.parent = allCarts.transform;
			player2.transform.parent = allCarts.transform;

			player1.tag = "player1";
			player2.tag = "player2";

			script1.target = player1.transform;
			script2.target = player2.transform;

			Canvas c1 = (Canvas) GameObject.Find("PlayerCanvases").transform.FindChild("p1Canvas").GetComponent<Canvas> ();
			c1.worldCamera = (Camera) camera12p.GetComponent<Camera>();
			c1.gameObject.SetActive (true);
			c1.planeDistance = 1;

			Canvas c2 = (Canvas) GameObject.Find("PlayerCanvases").transform.FindChild("p2Canvas").GetComponent<Canvas> ();
			c2.worldCamera = (Camera) camera22p.GetComponent<Camera>();
			c2.gameObject.SetActive (true);
			c2.planeDistance = 1;

		} else if (SceneConfig.players == 3) {
			camera13p.gameObject.SetActive (true);
			camera23p.gameObject.SetActive (true);
			camera33p.gameObject.SetActive (true);

			player1 = (GameObject) Instantiate(car, position1, Quaternion.identity);
			player2 = (GameObject) Instantiate(car, position2, Quaternion.identity);
			player3 = (GameObject) Instantiate(car, position3, Quaternion.identity);

			setColorForCart(player1, SceneConfig.playersColors[1]);
			setColorForCart(player2, SceneConfig.playersColors[2]);
			setColorForCart(player3, SceneConfig.playersColors[3]);

			CarController controller1 = (CarController) player1.GetComponent ("CarController");
			CarController controller2 = (CarController) player2.GetComponent ("CarController");
			CarController controller3 = (CarController) player3.GetComponent ("CarController");

			controller1.xaxis = SceneConfig.player1ControlCode + "_X axis";
			controller1.yaxis = SceneConfig.player1ControlCode + "_RT";

			if (SceneConfig.player1ControlCode.StartsWith ("C")) {
				controller1.brakeaxis = SceneConfig.player1ControlCode + "_LT";
				controller1.fireButton = SceneConfig.player1ControlCode + "_Fire";
			} else {
				controller1.fireButton = SceneConfig.player1ControlCode + "_Start";
			}

			controller2.xaxis = SceneConfig.player2ControlCode + "_X axis";
			controller2.yaxis = SceneConfig.player2ControlCode + "_RT";

			if (SceneConfig.player2ControlCode.StartsWith ("C")) {
				controller2.brakeaxis = SceneConfig.player2ControlCode + "_LT";
				controller2.fireButton = SceneConfig.player2ControlCode + "_Fire";
			} else {
				controller2.fireButton = SceneConfig.player2ControlCode + "_Start";
			}

			controller3.xaxis = SceneConfig.player3ControlCode + "_X axis";
			controller3.yaxis = SceneConfig.player3ControlCode + "_RT";

			if (SceneConfig.player3ControlCode.StartsWith ("C")) {
				controller3.brakeaxis = SceneConfig.player3ControlCode + "_LT";
				controller3.fireButton = SceneConfig.player3ControlCode + "_Fire";
			} else {
				controller3.fireButton = SceneConfig.player3ControlCode + "_Start";
			}



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

			Canvas c1 = (Canvas) GameObject.Find("PlayerCanvases").transform.FindChild("p1Canvas").GetComponent<Canvas> ();
			c1.worldCamera = (Camera) camera13p.GetComponent<Camera>();
			c1.gameObject.SetActive (true);
			c1.planeDistance = 1;

			Canvas c2 = (Canvas) GameObject.Find("PlayerCanvases").transform.FindChild("p2Canvas").GetComponent<Canvas> ();
			c2.worldCamera = (Camera) camera23p.GetComponent<Camera>();
			c2.gameObject.SetActive (true);
			c2.planeDistance = 1;

			Canvas c3 = (Canvas) GameObject.Find("PlayerCanvases").transform.FindChild("p3Canvas").GetComponent<Canvas> ();
			c3.worldCamera = (Camera) camera33p.GetComponent<Camera>();
			c3.gameObject.SetActive (true);
			c3.planeDistance = 1;

		} else if (SceneConfig.players == 4) {
			camera14p.gameObject.SetActive (true);
			camera24p.gameObject.SetActive (true);
			camera34p.gameObject.SetActive (true);
			camera44p.gameObject.SetActive (true);

			player1 = (GameObject) Instantiate(car, position1, Quaternion.identity);
			player2 = (GameObject) Instantiate(car, position2, Quaternion.identity);
			player3 = (GameObject) Instantiate(car, position3, Quaternion.identity);
			player4 = (GameObject) Instantiate(car, position4, Quaternion.identity);

			setColorForCart(player1, SceneConfig.playersColors[1]);
			setColorForCart(player2, SceneConfig.playersColors[2]);
			setColorForCart(player3, SceneConfig.playersColors[3]);
			setColorForCart(player4, SceneConfig.playersColors[4]);

			CarController controller1 = (CarController) player1.GetComponent ("CarController");
			CarController controller2 = (CarController) player2.GetComponent ("CarController");
			CarController controller3 = (CarController) player3.GetComponent ("CarController");
			CarController controller4 = (CarController) player4.GetComponent ("CarController");

			controller1.xaxis = SceneConfig.player1ControlCode + "_X axis";
			controller1.yaxis = SceneConfig.player1ControlCode + "_RT";

			if (SceneConfig.player1ControlCode.StartsWith ("C")) {
				controller1.brakeaxis = SceneConfig.player1ControlCode + "_LT";
				controller1.fireButton = SceneConfig.player1ControlCode + "_Fire";
			} else {
				controller1.fireButton = SceneConfig.player1ControlCode + "_Start";
			}

			controller2.xaxis = SceneConfig.player2ControlCode + "_X axis";
			controller2.yaxis = SceneConfig.player2ControlCode + "_RT";

			if (SceneConfig.player2ControlCode.StartsWith ("C")) {
				controller2.brakeaxis = SceneConfig.player2ControlCode + "_LT";
				controller2.fireButton = SceneConfig.player2ControlCode + "_Fire";
			} else {
				controller2.fireButton = SceneConfig.player2ControlCode + "_Start";
			}

			controller3.xaxis = SceneConfig.player3ControlCode + "_X axis";
			controller3.yaxis = SceneConfig.player3ControlCode + "_RT";
			if (SceneConfig.player3ControlCode.StartsWith ("C")) {
				controller3.brakeaxis = SceneConfig.player3ControlCode + "_LT";
				controller3.fireButton = SceneConfig.player3ControlCode + "_Fire";
			} else {
				controller3.fireButton = SceneConfig.player3ControlCode + "_Start";
			}

			controller4.xaxis = SceneConfig.player4ControlCode + "_X axis";
			controller4.yaxis = SceneConfig.player4ControlCode + "_RT";

			if (SceneConfig.player4ControlCode.StartsWith ("C")) {
				controller4.brakeaxis = SceneConfig.player4ControlCode + "_LT";
				controller4.fireButton = SceneConfig.player4ControlCode + "_Fire";
			} else {
				controller4.fireButton = SceneConfig.player4ControlCode + "_Start";
			}



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

			Canvas c1 = (Canvas) GameObject.Find("PlayerCanvases").transform.FindChild("p1Canvas").GetComponent<Canvas> ();
			c1.worldCamera = (Camera) camera14p.GetComponent<Camera>();
			c1.gameObject.SetActive (true);
			c1.planeDistance = 1;

			Canvas c2 = (Canvas) GameObject.Find("PlayerCanvases").transform.FindChild("p2Canvas").GetComponent<Canvas> ();
			c2.worldCamera = (Camera) camera24p.GetComponent<Camera>();
			c2.gameObject.SetActive (true);
			c2.planeDistance = 1;

			Canvas c3 = (Canvas) GameObject.Find("PlayerCanvases").transform.FindChild("p3Canvas").GetComponent<Canvas> ();
			c3.worldCamera = (Camera) camera34p.GetComponent<Camera>();
			c3.gameObject.SetActive (true);
			c3.planeDistance = 1;

			Canvas c4 = (Canvas) GameObject.Find("PlayerCanvases").transform.FindChild("p4Canvas").GetComponent<Canvas> ();
			c4.worldCamera = (Camera) camera44p.GetComponent<Camera>();
			c4.gameObject.SetActive (true);
			c4.planeDistance = 1;
		}
//		raceManager = (GameObject)Instantiate (raceManagerPrefab);
		PositionAndLapManager lapsMgr = (PositionAndLapManager)raceManager.GetComponent ("PositionAndLapManager");
		lapsMgr.setNumPlayers (SceneConfig.players);

		setFinishLine (finishTag);
	}

	void setFinishLine(string tag) {
		foreach (Transform cart in allCarts.transform) {
			cart.GetComponent<CartPosition> ().setLastWaypoint (GameObject.FindWithTag ("finishLine").transform);
		}
	}
	void Awake() {
		DontDestroyOnLoad(this);
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

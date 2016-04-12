using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneConfig : MonoBehaviour {
	public GameObject car;
	public Camera camera1p;
	public Camera camera12p;
	public Camera camera22p;
	public Camera camera13p;
	public Camera camera23p;
	public Camera camera33p;
	public Camera camera14p;
	public Camera camera24p;
	public Camera camera34p;
	public Camera camera44p;
	public static int score;

	public static int players;

	void Start() {
		if (SceneManager.GetActiveScene().name == "game") {
//			camera1p.enabled = false;
//			camera12p.enabled = false;
//			camera22p.enabled = false;
//			camera13p.enabled = false;
//			camera23p.enabled = false;
//			camera33p.enabled = false;
//			camera14p.enabled = false;
//			camera24p.enabled = false;
//			camera34p.enabled = false;
//			camera44p.enabled = false;
			if (players == 1) {
				camera1p.enabled = true;
				
				Camera cam = (Camera) camera1p.GetComponent("Camera");
				cam.tag = "MainCamera";
				cam.enabled = true ;
				GameObject player = Instantiate(car);
				TrackObject script = (TrackObject) camera1p.GetComponent("TrackObject");

				script.target = player.transform;

			} else if (players == 2) {
				
				GameObject player1 = Instantiate(car);
				GameObject player2 = Instantiate(car);

				TrackObject script1 = (TrackObject) camera12p.GetComponent("TrackObject");
				TrackObject script2 = (TrackObject) camera22p.GetComponent("TrackObject");

				script1.target = player1.transform;
				script2.target = player2.transform;

			} else if (players == 3) {
				GameObject player1 = Instantiate(car);
				GameObject player2 = Instantiate(car);
				GameObject player3 = Instantiate(car);

				TrackObject script1 = (TrackObject) camera13p.GetComponent("TrackObject");
				TrackObject script2 = (TrackObject) camera23p.GetComponent("TrackObject");
				TrackObject script3 = (TrackObject) camera33p.GetComponent("TrackObject");

				script1.target = player1.transform;
				script2.target = player2.transform;
				script3.target = player3.transform;

			} else if (players == 4) {
				GameObject player1 = Instantiate(car);
				GameObject player2 = Instantiate(car);
				GameObject player3 = Instantiate(car);
				GameObject player4 = Instantiate(car);

				TrackObject script1 = (TrackObject) camera14p.GetComponent("TrackObject");
				TrackObject script2 = (TrackObject) camera24p.GetComponent("TrackObject");
				TrackObject script3 = (TrackObject) camera34p.GetComponent("TrackObject");
				TrackObject script4 = (TrackObject) camera44p.GetComponent("TrackObject");

				script1.target = player1.transform;
				script2.target = player2.transform;
				script3.target = player3.transform;
				script4.target = player4.transform;
			}
//			Camera player1camera = Instantiate(topCamera2p);

//			Component com = GetComponent ("TrackObject");
//			com.pa
//			player1camera.gameObject.AddComponent (com);
			for (int x = 0; x < players; x++) {
//				GameObject player = Instantiate(car);
//				cube.AddComponent<Rigidbody>();
//				cube.transform.position = new Vector3(x, y, 0);
			}
		}
	}

	void Awake()
	{
		DontDestroyOnLoad(this);
	}

	public void LoadLevel(string level){
		Application.LoadLevel(level);

	}

	public void setPlayers (Slider slider){
		players = (int) slider.value;
	}
}
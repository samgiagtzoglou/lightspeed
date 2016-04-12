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

	void Start() {
		if (SceneConfig.players == 0) {
			SceneConfig.players = 1;
		}
		if (SceneConfig.players == 1) {
			camera1p.gameObject.SetActive (true);

			GameObject player = Instantiate(car);
			TrackObject script = (TrackObject) camera1p.GetComponent("TrackObject");

			script.target = player.transform;

		} else if (SceneConfig.players == 2) {
				
			camera12p.gameObject.SetActive (true);
			camera22p.gameObject.SetActive (true);

			GameObject player1 = Instantiate(car);
			GameObject player2 = Instantiate(car);

			TrackObject script1 = (TrackObject) camera12p.GetComponent("TrackObject");
			TrackObject script2 = (TrackObject) camera22p.GetComponent("TrackObject");

			script1.target = player1.transform;
			script2.target = player2.transform;

		} else if (SceneConfig.players == 3) {
			camera13p.gameObject.SetActive (true);
			camera23p.gameObject.SetActive (true);
			camera33p.gameObject.SetActive (true);

			GameObject player1 = Instantiate(car);
			GameObject player2 = Instantiate(car);
			GameObject player3 = Instantiate(car);

			TrackObject script1 = (TrackObject) camera13p.GetComponent("TrackObject");
			TrackObject script2 = (TrackObject) camera23p.GetComponent("TrackObject");
			TrackObject script3 = (TrackObject) camera33p.GetComponent("TrackObject");

			script1.target = player1.transform;
			script2.target = player2.transform;
			script3.target = player3.transform;

		} else if (SceneConfig.players == 4) {
			camera14p.gameObject.SetActive (true);
			camera24p.gameObject.SetActive (true);
			camera34p.gameObject.SetActive (true);
			camera44p.gameObject.SetActive (true);

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
	}


	void Awake()
	{
		DontDestroyOnLoad(this);
	}


		
}
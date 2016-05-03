using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PolePosition : MonoBehaviour {
	public static Dictionary<int, int> finishOrder = new Dictionary<int, int>{{1,0},{2,0},{3,0},{4,0}};
	public static int playersFinished = 0;

	// Use this for initialization
	void Start () {
//		for (int i = 1; i <= SceneConfig.players; i++) {
//			GameObject places = GameObject.Find("p" + i.ToString() + "Canvas").transform.FindChild("Places").gameObject;
//
//			for (int j = 1; j <= SceneConfig.players; j++) {
//				places.transform.FindChild (j.ToString()).gameObject.SetActive(true);
//		
//			}
//		}
	}
	public void finish(int player) {
		if (finishOrder [player] == 0) {
			playersFinished++;
			int place = playersFinished;
			Debug.Log ("Player " + player + " finished " + place);
			finishOrder [player] = place;
			GameObject places = GameObject.Find ("p" + player.ToString () + "Canvas").transform.FindChild ("Places").gameObject;
			places.SetActive (true);
		}
	}
	// Update is called once per frame
	void Update () {
//		for (int i = 1; i <= SceneConfig.players; i++) {
//			if (finishOrder[i] > 0) {
//				
//			}
//		}
	}
}
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LapManager : MonoBehaviour {

	public GameObject startCheckpoint;
	public GameObject midCheckpoint;

	private Collider startCollider;
	private Collider midCollider;

	private Collider nextCollider;
	private int lap;
	public Text lapText;

	// Use this for initialization
	void Start () {
		lap = 1;
		startCollider = startCheckpoint.GetComponent<Collider>();
		midCollider = midCheckpoint.GetComponent<Collider>();
		nextCollider = midCollider;
		Debug.Log (nextCollider);
	}
	
	// Update is called once per frame
	void Update () {
		lapText.text = "Lap: " + lap;
	}

	void OnTriggerEnter(Collider other) {
		if (other == midCollider && nextCollider == midCollider) {
			nextCollider = startCollider;
			Debug.Log (nextCollider);
		} else if (other == startCollider && nextCollider == startCollider) {
			nextCollider = midCollider;
			lap++;
			Debug.Log (nextCollider);
		}
	}
}

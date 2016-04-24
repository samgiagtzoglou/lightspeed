using UnityEngine;
using System.Collections;

public class CartPosition : MonoBehaviour {

	public int currentWaypoint;
	public int currentLap;
	public Transform lastWaypoint;
	public int nbWaypoint; //Set the amount of Waypoints

	private static int WAYPOINT_VALUE = 100;
	private static int LAP_VALUE = 10000;
	private int cpt_waypoint = 0;

	private Vector3 respawnPosition;
	private Quaternion respawnRotation;

	// Use this for initialization
	void Start() {
		currentWaypoint = 0;
		currentLap = 0;
		cpt_waypoint = 0;
		respawnPosition = (Vector3) GameObject.Find ("firstStart").transform.position;
		respawnRotation = Quaternion.identity;
	}

	public void OnTriggerEnter(Collider other) {
		string checkpointNum = other.gameObject.name;
		//Not the ideal system. Convert to tag-based code at some point
		try {
			currentWaypoint = System.Convert.ToInt32(checkpointNum);
			respawnPosition = this.transform.position;
			respawnRotation = this.transform.rotation;

			if (currentWaypoint == 1 && cpt_waypoint == nbWaypoint) { // completed a lap, so increase currentLap;
				currentLap++;
				cpt_waypoint = 0;
			}
			if (cpt_waypoint == currentWaypoint - 1) {
				lastWaypoint = other.transform;
				cpt_waypoint++;
			}
		} catch {
//			Debug.Log ("Not a checkpoint!");
		}
	}

	public float GetDistance() {
		return (transform.position - lastWaypoint.position).magnitude + currentWaypoint * WAYPOINT_VALUE + currentLap * LAP_VALUE;
	}

	public void setLastWaypoint(Transform wpt) {
		lastWaypoint = wpt;
	}

	public int GetCarPosition(CartPosition[] allCars) {
		float distance = GetDistance();
		int position = 1;
		foreach (CartPosition pos in allCars) {
			if (pos.GetDistance() > distance)
				position++;
		}
		return position;
	}

	public Vector3 getRespawnPosition() {
		Debug.Log (respawnPosition);
		return respawnPosition;
	}

	public Quaternion getRespawnRotation () {
		return respawnRotation;
	}
}
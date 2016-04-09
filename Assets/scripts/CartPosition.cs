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

	// Use this for initialization
	public void Initialize() {
		currentWaypoint = 0;
		currentLap = 0;
		cpt_waypoint = 0;
	}

	public void OnTriggerEnter(Collider other) {
		string checkpointNum = other.gameObject.name;
		Debug.Log (checkpointNum);
		currentWaypoint = System.Convert.ToInt32(checkpointNum);
		if (currentWaypoint == 1 && cpt_waypoint == nbWaypoint) { // completed a lap, so increase currentLap;
			currentLap++;
			cpt_waypoint = 0;
		}
		if (cpt_waypoint == currentWaypoint - 1) {
			lastWaypoint = other.transform;
			cpt_waypoint++;
		}
	}

	public float GetDistance() {
		return (transform.position - lastWaypoint.position).magnitude + currentWaypoint * WAYPOINT_VALUE + currentLap * LAP_VALUE;
	}

	public int GetCarPosition(CartPosition[] allCars) {
		float distance = GetDistance();
		//Debug.Log (gameObject.name + " distance " + distance);
		int position = 1;
		foreach (CartPosition pos in allCars) {
			if (pos.GetDistance() > distance)
				position++;
		}
		//Debug.Log (gameObject.name + " at position " + position);
		return position;
	}
}
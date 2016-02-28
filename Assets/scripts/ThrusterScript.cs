using UnityEngine;
using System.Collections;

public class ThrusterScript : MonoBehaviour {
	public float thrusterStrength;
	public float thrusterDistance;
	public Transform[] thrusters;
	private Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate() {
		RaycastHit hit;

		foreach (Transform thruster in thrusters) {
			Vector3 downwardForce;
			float distancePercentage;

			if (Physics.Raycast (thruster.position, thruster.up * -1, out hit, thrusterDistance)) {
				//The thruster is within thrusterdistance to the ground. How far away?
				distancePercentage = 1 - (hit.distance / thrusterDistance);
				//Calculate how much force to push
				downwardForce = transform.up * thrusterStrength * distancePercentage;
				//Correct the force for mass of car and deltaTime
				downwardForce = downwardForce * Time.deltaTime * rb.mass;
				//Apply the force where the thrusters are
				rb.AddForceAtPosition (downwardForce, thruster.position);
			}
		}
	}
}

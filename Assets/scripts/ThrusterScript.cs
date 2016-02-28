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
				distancePercentage = 1 - (hit.distance / thrusterDistance);
				downwardForce = transform.up * thrusterStrength * distancePercentage;
				downwardForce = downwardForce * Time.deltaTime * rb.mass;
				rb.AddForceAtPosition (downwardForce, thruster.position);
			}
		}
	}
}

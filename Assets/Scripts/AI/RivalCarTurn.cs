using UnityEngine;
using System.Collections;

public class RivalCarTurn : MonoBehaviour {
	private Vector3[] path;
	//Values that control the vehicle
	public float acceleration;
	public float rotationRate;

	//Values for faking a nice turn display
	public float turnRotationAngle;
	public float turnRotationSeekSpeed;

	//Reference variables we don't directly use
	public float rotationVelocity;
	public float groundAngleVelocity;
	private Rigidbody rb;
	private int currentPathObj = 0;
	public float maxSpeed;

	public float distFromPath = 30f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		Transform orig = GameObject.Find ("Path").transform;
		path = new Vector3[orig.childCount];
		int i = 0;
		foreach (Transform child in orig) {
			path [i] = child.position;
			i++;
		}
	}
	
	void FixedUpdate() {
		Vector3 steerVector = transform.InverseTransformPoint (path [currentPathObj].x, path [currentPathObj].y, path [currentPathObj].z);
		float newSteer = (steerVector.x / steerVector.magnitude);
		//Debug.Log (newSteer);
		//Debug.Log(steerVector.magnitude);
		if (steerVector.magnitude < distFromPath) {
			currentPathObj++;
		}
		if (currentPathObj >= path.Length) {
			currentPathObj = 0;
		}

		if (Physics.Raycast(transform.position, transform.up*-1, 3f)) {
			
			//We are on the ground. Enable the accelerator and increase drag.
			rb.drag = 1;
			Vector3 forwardForce = transform.forward * acceleration * 0.1f;
			//Correct force for deltatime and vehicle mass
			forwardForce = forwardForce * Time.deltaTime * rb.mass;
			rb.AddForce(forwardForce);
		} else {
			rb.drag = 0;
		}

		//You can turn in the air or on the ground
		Vector3 turnTorque = Vector3.up * rotationRate * newSteer;
		//Correct force for deltatime and vehiclemass
		turnTorque = turnTorque * Time.deltaTime * rb.mass;
		rb.AddTorque (turnTorque);

		//"Fake" rotate the car when you are turning
		Vector3 newRotation = transform.eulerAngles;
		newRotation.z = Mathf.SmoothDampAngle (newRotation.z, newSteer * -turnRotationAngle, ref rotationVelocity, turnRotationSeekSpeed);
		transform.eulerAngles = newRotation;
	}
}

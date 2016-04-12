using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CarController : MonoBehaviour {
	//Values that control the vehicle
	public float acceleration;
	public float rotationRate;

	//Values for faking a nice turn display
	public float turnRotationAngle;
	public float turnRotationSeekSpeed;

	//Reference variables we don't directly use
	public float rotationVelocity;
	public float groundAngleVelocity;

	private bool drivingAllowed;
	private bool inOrbit;

	public string xaxis;
	public string yaxis;

	private Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		inOrbit = false;
		drivingAllowed = true;
	}

	public void startDriving() {
		drivingAllowed = true;
	}

	void FixedUpdate() {
		if (yaxis != "") {
			Debug.Log ("Connecting to " + yaxis); 
		if (!inOrbit && drivingAllowed) {
			//Check if we are touching the ground
			if (Physics.Raycast(transform.position, transform.up*-1, 3f)) {
				//We are on the ground. Enable the accelerator and increase drag.
				rb.drag = 1;
				Vector3 forwardForce = transform.forward * acceleration * Input.GetAxis(yaxis);
				//Correct force for deltatime and vehicle mass
				forwardForce = forwardForce * Time.deltaTime * rb.mass;
				rb.AddForce(forwardForce);
			} else {
				rb.drag = 0;
			}

			//You can turn in the air or on the ground
			Vector3 turnTorque = Vector3.up * rotationRate * Input.GetAxis (xaxis);
			//Correct force for deltatime and vehiclemass
			turnTorque = turnTorque * Time.deltaTime * rb.mass;
			rb.AddTorque (turnTorque);

			//"Fake" rotate the car when you are turning
			Vector3 newRotation = transform.eulerAngles;
			newRotation.z = Mathf.SmoothDampAngle (newRotation.z, Input.GetAxis (xaxis) * -turnRotationAngle, ref rotationVelocity, turnRotationSeekSpeed);
			transform.eulerAngles = newRotation;
		}	
	}
	}

	public void EnterOrbit() {
		inOrbit = true;
		//foreach (Renderer r in GetComponentsInChildren<Renderer>()) r.enabled = false;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.Sleep();
	}

	public void LeaveOrbit() {
		inOrbit = false;
		//foreach (Renderer r in GetComponentsInChildren<Renderer>()) r.enabled = true;
	}
}

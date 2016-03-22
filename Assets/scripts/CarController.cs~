using UnityEngine;
using System.Collections;

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

	private Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody> ();
	}

	// void OnCollisionEnter(Collision collision) {
	// 	ContactPoint contact;
	// 	contact = collision.contacts[0];
	// 	float dot = Vector3.Dot(contact.normal, (-transform.forward));
	// 	dot *= 2;
	// 	Vector3 reflection = contact.normal * dot;
	// 	reflection = reflection + transform.forward;
	// 	rb.velocity = rb.transform.TransformDirection(reflection.normalized * 15.0);
	// }

	void FixedUpdate() {
		//Check if we are touching the ground
		if (Physics.Raycast(transform.position, transform.up*-1, 3f)) {
			//We are on the ground. Enable the accelerator and increase drag.
			rb.drag = 1;
			Vector3 forwardForce = transform.forward * acceleration * Input.GetAxis("Vertical");
			//Correct force for deltatime and vehicle mass
			forwardForce = forwardForce * Time.deltaTime * rb.mass;
			rb.AddForce(forwardForce);
		} else {
			rb.drag = 0;
		}

		//You can turn in the air or on the ground
		Vector3 turnTorque = Vector3.up * rotationRate * Input.GetAxis ("Horizontal");
		//Correct force for deltatime and vehiclemass
		turnTorque = turnTorque * Time.deltaTime * rb.mass;
		rb.AddTorque (turnTorque);

		// if (transform.position.y <= 0.45f) {
		// 	Vector3 newPos = new Vector3(transform.position.x, 0.45f, transform.position.z);
		// 	rb.MovePosition (newPos);
		// }

		//"Fake" rotate the car when you are turning
		Vector3 newRotation = transform.eulerAngles;
		newRotation.z = Mathf.SmoothDampAngle (newRotation.z, Input.GetAxis ("Horizontal") * -turnRotationAngle, ref rotationVelocity, turnRotationSeekSpeed);
		transform.eulerAngles = newRotation;
	}
}

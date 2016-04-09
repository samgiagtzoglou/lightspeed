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

	// powerup variables
	private bool hasPowerup;

	// Black hole powerup variables
	public float blackHoleOrbitRadius;
	public float blackHoleOrbitSpeed;
	public GameObject blackHolePrefab;

	private Vector3 orbitCenter;
	private float bhOrbitTime;
	private float bhOrbitInitialPhase;

	private bool drivingAllowed;
	private bool inElectronOrbit;
	private bool inBlackHoleOrbit;

	private Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		inElectronOrbit = false;
		drivingAllowed = true;
		hasPowerup = true;
	}

	public void startDriving() {
		drivingAllowed = true;
	}

	private void DropBlackHole() {
		if (hasPowerup) {
			Instantiate(blackHolePrefab, transform.position - (10.0f * transform.forward),
						Quaternion.identity);
			hasPowerup = false;
		}
	}

	void Update() {
		if (Input.GetButton("Fire1")) {
			DropBlackHole();
		}
	}

	void FixedUpdate() {
		if (!inElectronOrbit && !inBlackHoleOrbit && drivingAllowed) {
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

			//"Fake" rotate the car when you are turning
			Vector3 newRotation = transform.eulerAngles;
			newRotation.z = Mathf.SmoothDampAngle (newRotation.z, Input.GetAxis ("Horizontal") * -turnRotationAngle, ref rotationVelocity, turnRotationSeekSpeed);
			transform.eulerAngles = newRotation;
		} else if (inBlackHoleOrbit) {
			float orbitPhase = (Time.time - bhOrbitTime) * blackHoleOrbitSpeed +
				bhOrbitInitialPhase;
			transform.position =
				(new Vector3 (blackHoleOrbitRadius * Mathf.Cos(orbitPhase)
							  + orbitCenter.x,
							  transform.position.y, 
							  blackHoleOrbitRadius * Mathf.Sin(orbitPhase)
							  + orbitCenter.z));
			transform.rotation = Quaternion.LookRotation
				(new Vector3 (-Mathf.Sin(orbitPhase), 0.0f, 
							  Mathf.Cos(orbitPhase)));
		}
	}

	public void EnterAtomOrbit() {
		inElectronOrbit = true;
		//foreach (Renderer r in GetComponentsInChildren<Renderer>()) r.enabled = false;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.Sleep();
	}

	public void LeaveAtomOrbit() {
		inElectronOrbit = false;
		//foreach (Renderer r in GetComponentsInChildren<Renderer>()) r.enabled = true;
	}

	public void EnterBlackHoleOrbit(Vector3 center) {
		inBlackHoleOrbit = true;
		bhOrbitTime = Time.time;
		bhOrbitInitialPhase = Mathf.Acos(center.x / center.magnitude);
		orbitCenter = center;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.Sleep();
		transform.rotation = Quaternion.LookRotation(new Vector3
													 (center.x, 0.0f,
													  center.z));
	}

	public void LeaveBlackHoleOrbit() {
		inBlackHoleOrbit = false;
	}
}

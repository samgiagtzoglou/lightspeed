using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CarController : MonoBehaviour {
	// Powerup enum
	enum Powerups {none, blackhole, shield, attack, boost}
	private Powerups powerup;

	//Values that control the vehicle
	public float acceleration;
	public float rotationRate;
	public float position;
	public float totalRacers;

	//Values for faking a nice turn display
	public float turnRotationAngle;
	public float turnRotationSeekSpeed;

	//Reference variables we don't directly use
	public float rotationVelocity;
	public float groundAngleVelocity;

	// Black hole powerup variables
	public float blackHoleOrbitRadius;
	public float blackHoleOrbitSpeed;
	public GameObject blackHolePrefab;

	private Vector3 orbitCenter;
	private float bhOrbitTime;
	private float bhOrbitInitialPhase;

	// Shield powerup variables
	public ShieldController[] shieldControllers;

	private bool shieldsUp;
	private bool drivingAllowed;
	private bool inElectronOrbit;
	private bool inBlackHoleOrbit;

	private Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		inElectronOrbit = false;
		drivingAllowed = true;
		shieldsUp = false;
		powerup = Powerups.shield;
	}

	public void startDriving() {
		drivingAllowed = true;
	}

	private void DropBlackHole() {
		Instantiate(blackHolePrefab, transform.position - (10.0f * transform.forward),
					Quaternion.identity);
	}

	private void ShieldsUp() {
		foreach (ShieldController shield in shieldControllers) shield.Enable();
		shieldsUp = true;
	}

	private void ShieldsDown() {
		foreach (ShieldController shield in shieldControllers) shield.Disable();
		shieldsUp = false;
	}	

	void Update() {
		if (Input.GetButton("Fire1")) {
			switch (powerup) {
				case Powerups.blackhole:
					DropBlackHole();
					powerup = Powerups.none;
					break;
				case Powerups.shield:
					ShieldsUp();
					powerup = Powerups.none;
					break;
				default:
					break;
			}
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
		if (!shieldsUp) {
			inBlackHoleOrbit = true;
			bhOrbitTime = Time.time;
			bhOrbitInitialPhase = 0.0f;
			orbitCenter = center;
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			rb.Sleep();
			transform.rotation = Quaternion.LookRotation(new Vector3
														 (center.x, 0.0f,
														  center.z));
		} else {
			ShieldsDown();
		}
	}

	public void LeaveBlackHoleOrbit() {
		inBlackHoleOrbit = false;
	}

	void OnTriggerEnter(Collider other) {
		if (other.name == "Item Box") {
			if (powerup == Powerups.none) {
				float success = 1.0f - ((float) (position - 1) / (float) (totalRacers - 1));
				
				// create weights out of 1.0 for each powerup
				float blackhole = 0.5f * success;
				float boost = blackhole + (0.5f - 0.5f * success);
				float shield = boost + (0.125f + 0.25f * success);
				// attack is the last remaining amount between shield and 1.0

				float powerupIndex = Random.Range(0.0f, 1.0f);
				if (powerupIndex < blackhole) {
					powerup = Powerups.blackhole;
				} else if (powerupIndex < boost) {
					powerup = Powerups.boost;
				} else if (powerupIndex < shield) {
					powerup = Powerups.shield;
				} else {
					powerup = Powerups.attack;
				}
			}
		}
	}
}

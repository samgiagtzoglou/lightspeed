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

	// Handling travelling in medium
	public int wavelength;
	private bool inMedium;
	public float maxMediumAccelerationReduction;
	public float maxMediumSpeed;
	public float maxMediumSpeedReduction;

	// Handling travelling in the track
	public float maxTrackAccelerationReduction;
	public float maxTrackSpeed;
	public float maxTrackSpeedReduction;

	// Boost powerup variables
	public float boostStrength;
	public float boostTime;

	private float boostStartTime;

	// Black hole powerup variables
	public float blackHoleOrbitRadius;
	public float blackHoleOrbitSpeed;
	public GameObject blackHolePrefab;

	private Vector3 orbitCenter;
	private float bhOrbitTime;
	private float bhOrbitInitialPhase;

	// Lightgun powerup variables
	public GameObject lightBallPrefab;
	public float attackSpeedReduction;
	public float attackLength;

	private float attackStartTime;

	// Shield powerup variables
	public GameObject[] shieldObjects;
	private float shieldStartTime;

	public float shieldTime;

	public  bool shieldsUp;
	private bool drivingAllowed;
	private bool inElectronOrbit;
	private bool inBlackHoleOrbit;

	public string xaxis;
	public string yaxis;
	public string brakeaxis;
	public string fireButton;

	//Powerup icons
	public Sprite shieldIcon;
	public Sprite attackIcon;
	public Sprite blackholeIcon;
	public Sprite boostIcon;

	private Sprite powerupSprite;

	//Canvas
	public Canvas myCanvas;

	private Rigidbody rb;
	public WaveTailController waveTailController;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		inElectronOrbit = false;
		drivingAllowed = false;
		shieldsUp = false;
		inMedium = false;
		attackStartTime = 0f;
		powerup = Powerups.attack;
	}
		
	public void startDriving() {
		drivingAllowed = true;
	}

	private void ActivateBoost() {
		boostStartTime = Time.time;
	}

	private void DropBlackHole() {
		Instantiate(blackHolePrefab, transform.position - (10.0f * transform.forward),
			Quaternion.identity);
	}

	private void ShootLightGun() {
		GameObject bullet = (GameObject) Instantiate(lightBallPrefab,
			transform.position + (3.0f * transform.forward),
			Quaternion.identity);
	}

	private void ShieldsUp() {
		foreach (GameObject shield in shieldObjects) {
			shield.SetActive (true);
		}
		shieldsUp = true;
		shieldStartTime = Time.time;
	}

	public void ShieldsDown() {
		foreach (GameObject shield in shieldObjects) shield.SetActive(false);
		shieldsUp = false;
	}	

	void Update() {
		if (Input.GetButton(fireButton)) {
			Debug.Log (fireButton + " : " + powerup);
			switch (powerup) {
			case Powerups.blackhole:
				DropBlackHole();
				powerup = Powerups.none;
				break;
			case Powerups.shield:
				ShieldsUp();
				powerup = Powerups.none;
				break;
			case Powerups.attack:
				ShootLightGun();
				powerup = Powerups.none;
				break;
			case Powerups.boost:
				ActivateBoost();
				powerup = Powerups.none;
				break;
			default:
				powerup = Powerups.none;
				break;
			}
		}
	}

	void FixedUpdate() {
		if (yaxis != "") {
			if (!inElectronOrbit && !inBlackHoleOrbit && drivingAllowed) {
				//Apply player input to the car
				applyDrivingForces ();

			} else if (inBlackHoleOrbit) {
				orbitBlackHole ();
			}
		}
		if (this.transform.position.y <= -40) {
			respawn ();
		}

		if (shieldsUp && (Time.time - shieldStartTime > shieldTime))
			ShieldsDown();
	}

	void orbitBlackHole() {
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

	void respawn() {
		CartPosition posHandler = GetComponent <CartPosition> ();
		this.transform.position = posHandler.getRespawnPosition ();
		this.transform.rotation = posHandler.getRespawnRotation ();
		this.GetComponent<Rigidbody> ().velocity = new Vector3(0f,0f,0f);
	}

	void applyDrivingForces() {
		//Check if we are touching the ground
		if (Physics.Raycast(transform.position, transform.up*-1, 3f)) {
			//We are on the ground. Enable the accelerator and increase drag.
			rb.drag = 1;
			float yfloat = 0;
			if (brakeaxis.EndsWith("LT")){

				//Controller, set axis for triggers
				float rt = (Input.GetAxis(yaxis)+1)/2;
				float lt = 0;
				Debug.Log ("Controller: " + rt + ", " + lt);
				if (Input.GetAxis (brakeaxis) != 0.0) {
					lt = (Input.GetAxis (brakeaxis) + 1) / (-2);
				}
				yfloat = (rt + lt);

			} else {
				yfloat = Input.GetAxis (yaxis);
				//Keyboard input

			}

			if (!inMedium) {
				float adjustedMaxSpeed = maxTrackSpeed - maxTrackSpeedReduction *
					(1.0f - ((float) (wavelength - 380) / 400.0f));
				if (Vector3.Magnitude(rb.velocity) < adjustedMaxSpeed) {
					float adjustedAcceleration = acceleration - maxTrackAccelerationReduction *
						((float) (wavelength - 380) / 400.0f);
					Vector3 forwardForce = transform.forward * adjustedAcceleration * yfloat;
					//Correct force for deltatime and vehicle mass
					forwardForce = forwardForce * Time.deltaTime * rb.mass;
					rb.AddForce(forwardForce);
				}

				if (Time.time - boostStartTime < boostTime)
					rb.AddForce(transform.forward * boostStrength * Time.deltaTime * rb.mass);
			} else {
				float adjustedMaxSpeed = maxMediumSpeed - maxMediumSpeedReduction *
					(1.0f - ((float) (wavelength - 380) / 400.0f));

				if (Time.time < attackStartTime + attackLength)
					adjustedMaxSpeed -= attackSpeedReduction;

				if (Vector3.Magnitude(rb.velocity) < adjustedMaxSpeed) {
					float adjustedAcceleration = acceleration - maxMediumAccelerationReduction *
						(1.0f - ((float) (wavelength - 380) / 400.0f));
					Vector3 forwardForce = transform.forward * adjustedAcceleration * yfloat;
					//Correct force for deltatime and vehicle mass
					forwardForce = forwardForce * Time.deltaTime * rb.mass;
					rb.AddForce(forwardForce);
				}
			}
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
		bhOrbitInitialPhase = 0.0f;
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

	private void drawPowerupIndicator(Powerups pwr) {
		if (pwr == Powerups.attack) {
			powerupSprite = attackIcon;
		} else if (pwr == Powerups.blackhole) {
			powerupSprite = blackholeIcon;
		} else if (pwr == Powerups.boost) {
			powerupSprite = boostIcon;
		} else {
			powerupSprite = shieldIcon;
		}
		Image img = myCanvas.transform.FindChild("Image").GetComponent<Image>();
		img.sprite = powerupSprite;
		img.color = new Color (255, 255, 255);
		Debug.Log (img.sprite);
	}

	public void OnTriggerEnter(Collider other) {
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
				drawPowerupIndicator (powerup);
			}
		} else if (other.name == "Medium") {
			inMedium = true;
			waveTailController.SetRefractiveIndex(1.5f);
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.name == "Medium") {
			inMedium = false;
			waveTailController.SetRefractiveIndex(1.0f);
		}
	}

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.tag == "homingBall"
			&& (Time.time > attackStartTime + attackLength + 1f)) {
			attackStartTime = Time.time;
		}
	}
}
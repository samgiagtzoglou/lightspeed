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

	// handle atom field
	private bool inField;
	public float maxVacuumSpeed;
	
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

	//Audio
	private AudioSource powerupSource;
	public AudioClip shieldSound;
	public AudioClip boostSound;
	public AudioClip laserSound;
	public AudioClip blackholeSound;
	public AudioClip pickupSound;

	//Animations
	private Color transparent;
	private Color opaque;
	public MovieTexture roidsAnimation;
	public MovieTexture plasmaAnimation;
	private MovieTexture animationTexture;
	public Sprite leftArrow;
	public Sprite rightArrow;
	private Image arrowImage;

	//Powerup icons
	public Sprite shieldIcon;
	public Sprite attackIcon;
	public Sprite blackholeIcon;
	public Sprite boostIcon;
	private Sprite powerupSprite;

	//Boost FX
	public GameObject myCamera;
	private bool boostActivated;

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
		inField = false;
		attackStartTime = 0f;
		powerup = Powerups.boost;
		boostActivated = false;
		opaque = new Color(255,255,255,255);
		transparent = new Color(255,255,255,0);
		powerupSource = this.transform.Find ("powerupEffects").GetComponent<AudioSource>();
	}

	Color wavelengthToColor(int newWavelength) {
		float factor, red, green, blue;
		float Gamma = 0.8f;
		float IntensityMax = 1.0f;
		if ((newWavelength >= 380) && (newWavelength<440)){
			red = -(float) (newWavelength - 440) / (440 - 380);
			green = 0.0f;
			blue = 1.0f;
		} else if((newWavelength >= 440) && (newWavelength<490)){
			red = 0.0f;
			green = (float) (newWavelength - 440) / (490 - 440);
			blue = 1.0f;
		} else if((newWavelength >= 490) && (newWavelength<510)){
			red = 0.0f;
			green = 1.0f;
			blue = -(float) (newWavelength - 510) / (510 - 490);
		} else if((newWavelength >= 510) && (newWavelength<580)){
			red = (float) (newWavelength - 510) / (580 - 510);
			green = 1.0f;
			blue = 0.0f;
		} else if((newWavelength >= 580) && (newWavelength<645)){
			red = 1.0f;
			green = -(float) (newWavelength - 645) / (645 - 580);
			blue = 0.0f;
		} else if((newWavelength >= 645) && (newWavelength<781)){
			red = 1.0f;
			green = 0.0f;
			blue = 0.0f;
		} else {
			red = 0.0f;
			green = 0.0f;
			blue = 0.0f;
		}
    
		if ((newWavelength >= 380) && (newWavelength<420)){
			factor = 0.3f + 0.7f * (float) (newWavelength - 380) / (420 - 380);
		} else if((newWavelength >= 420) && (newWavelength<701)) {
			factor = 1.0f;
		} else if((newWavelength >= 701) && (newWavelength<781)) {
			factor = 0.3f + 0.7f *(float) (780 - newWavelength) / (780 - 700);
		} else {
			factor = 0.0f;
		}
    
		if (red != 0){
			red = IntensityMax * Mathf.Pow(red * factor, Gamma);
		}
		if (green != 0){
			green = IntensityMax * Mathf.Pow(green * factor, Gamma);
		}
		if (blue != 0){
			blue = IntensityMax * Mathf.Pow(blue * factor, Gamma);
		}

		return new Color(red, green, blue, 0.8f);
	}	

	public void updateWavelength(int color) {
		wavelength = color;
		Renderer shieldRend = shieldObjects[0].GetComponent<Renderer>();
		shieldRend.material.SetColor("_EmissionColor", wavelengthToColor(color));
	}

	public void startDriving() {
		drivingAllowed = true;
	}

	private void ActivateBoost() {
		boostActivated = true;
		boostStartTime = Time.time;
		Transform speedupFX = myCamera.transform.Find ("SPEEDUPFX");
		ParticleSystem.EmissionModule prt = speedupFX.GetComponent<ParticleSystem> ().emission;
		prt.enabled = true;
		foreach (Transform warp in speedupFX) {
			prt = warp.GetComponent<ParticleSystem> ().emission;
			prt.enabled = true;
		}
		powerupSource.PlayOneShot (boostSound);
	}

	private void killBoostFX() {
		if (boostActivated) {
			boostActivated = false;
			Transform speedupFX = myCamera.transform.Find ("SPEEDUPFX");
			ParticleSystem.EmissionModule prt = speedupFX.GetComponent<ParticleSystem> ().emission;
			prt.enabled = false;
			foreach (Transform warp in speedupFX) {
				prt = warp.GetComponent<ParticleSystem> ().emission;
				prt.enabled = false;
			}
		} else {
			return;
		}
		Debug.Log ("ran the kill function");
	}

	private void DropBlackHole() {
		Instantiate(blackHolePrefab, transform.position - (10.0f * transform.forward),
			Quaternion.identity);
		powerupSource.PlayOneShot (blackholeSound);
	}

	private void ShootLightGun() {
		GameObject bullet = (GameObject) Instantiate(lightBallPrefab,
										transform.position + (3.0f * transform.forward),
										Quaternion.identity);
		HomingCSharp bulletHoming = (HomingCSharp) bullet.GetComponent("HomingCSharp");
		bulletHoming.launcher = this.gameObject;
		bulletHoming.launchCart = this.gameObject;
		powerupSource.PlayOneShot (laserSound);
	}

	private void ShieldsUp() {
		foreach (GameObject shield in shieldObjects) {
			shield.SetActive (true);
		}
		shieldsUp = true;
		shieldStartTime = Time.time;
		powerupSource.PlayOneShot (shieldSound);
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
			//Set the image area to transparent
			myCanvas.transform.Find ("Image").GetComponent<Image> ().sprite = null;
			Color newColor = new Color (0, 0, 0);
			newColor.a = 0;
			myCanvas.transform.Find ("Image").GetComponent<Image> ().color = newColor;
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

			if (!inMedium && !inField) {
				float adjustedMaxSpeed = maxTrackSpeed - maxTrackSpeedReduction *
					(1.0f - ((float) (wavelength - 380) / 400.0f));

				if (Time.time < attackStartTime + attackLength)
					adjustedMaxSpeed -= attackSpeedReduction;
				
				if (Vector3.Magnitude(rb.velocity) < adjustedMaxSpeed) {
					float adjustedAcceleration = acceleration - maxTrackAccelerationReduction *
						((float) (wavelength - 380) / 400.0f);
					Vector3 forwardForce = transform.forward * adjustedAcceleration * yfloat;
					//Correct force for deltatime and vehicle mass
					forwardForce = forwardForce * Time.deltaTime * rb.mass;
					rb.AddForce(forwardForce);
				}

				if (Time.time - boostStartTime < boostTime) {
					rb.AddForce (transform.forward * boostStrength * Time.deltaTime * rb.mass);
				} else {
					killBoostFX ();
				}
			} else if (inMedium) {
				float adjustedMaxSpeed = maxMediumSpeed - maxMediumSpeedReduction *
					(1.0f - ((float) (wavelength - 380) / 400.0f));
				
				if (Vector3.Magnitude(rb.velocity) < adjustedMaxSpeed) {
					float adjustedAcceleration = acceleration - maxMediumAccelerationReduction *
						(1.0f - ((float) (wavelength - 380) / 400.0f));
					Vector3 forwardForce = transform.forward * adjustedAcceleration * yfloat;
					//Correct force for deltatime and vehicle mass
					forwardForce = forwardForce * Time.deltaTime * rb.mass;
					rb.AddForce(forwardForce);
				}
			} else {
				if (Vector3.Magnitude(rb.velocity) < maxVacuumSpeed) {
					Vector3 forwardForce = transform.forward * acceleration * yfloat;
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
		//Draw a powerup icon
		Image img = myCanvas.transform.FindChild("Image").GetComponent<Image>();
		img.sprite = powerupSprite;
		img.color = new Color (255, 255, 255);
	}

	void BlinkArrow() {
		if (arrowImage.color == transparent) {
			arrowImage.color = opaque;
		} else {
			arrowImage.color = transparent;
		}
	}

	void startRoidsAnim() {
		RawImage rimg = myCanvas.transform.FindChild("Animation").GetComponent<RawImage>();
		Color notTransparant = new Color (255, 255, 255);
		notTransparant.a = 255;
		rimg.color = notTransparant;
		animationTexture = roidsAnimation;
		rimg.texture = animationTexture;
		Debug.Log(rimg.texture);
		animationTexture.Play ();
	}

	void startPlasmaAnim() {
		RawImage rimg = myCanvas.transform.FindChild("Animation").GetComponent<RawImage>();
		Color notTransparant = new Color (255, 255, 255);
		notTransparant.a = 255;
		rimg.color = notTransparant;
		animationTexture = plasmaAnimation;
		rimg.texture = animationTexture;
		Debug.Log(rimg.texture);
		animationTexture.Play ();
	}

	public void OnTriggerEnter(Collider other) {
		if (other.name == "PlasmaTrigger"){
			startPlasmaAnim();
		}
		if (other.name == "RoidsTrigger"){
			startRoidsAnim();
		}
		if (other.name == "ArrowTrigger") {
			arrowImage = myCanvas.transform.Find("Arrow").GetComponent<Image>();
			//Teal, green, purple and blue go to atom field
			if (wavelength == 490 || wavelength == 450 || wavelength == 400 || wavelength == 530) {
				arrowImage.sprite = rightArrow;
			} else {
				arrowImage.sprite = leftArrow;
			}
			arrowImage.color = opaque;
			InvokeRepeating ("BlinkArrow", 0f, .5f);
		}
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
				powerupSource.PlayOneShot (pickupSound);
			}
		} else if (other.name == "Medium") {
			inMedium = true;
			waveTailController.SetRefractiveIndex(2.5f);
		} else if (other.name == "Field") {
			inField = true;
			waveTailController.SetRefractiveIndex(1.0f);
		}
	}

	void stopMovieAnim() {
		animationTexture.Stop ();
		animationTexture = null;
		RawImage rimg = myCanvas.transform.FindChild("Animation").GetComponent<RawImage>();
		rimg.color = transparent;
	}

	void OnTriggerExit (Collider other) {
		if (other.name == "Medium") {
			inMedium = false;
			waveTailController.SetRefractiveIndex(1.5f);
		}
		if (other.name == "Field") {
			inField = false;
			waveTailController.SetRefractiveIndex(1.5f);
		}
		if (other.name == "PlasmaTrigger" || other.name == "RoidsTrigger") {
			stopMovieAnim ();
		}
		if (other.name == "ArrowTrigger") {
			CancelInvoke ("BlinkArrow");
			arrowImage.color = transparent;
		}
	}

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.tag == "homingBall"
			&& (Time.time > attackStartTime + attackLength + 1f)) {
			if (!shieldsUp) {
				attackStartTime = Time.time;
			} else {
				ShieldsDown();
			}
		}
	}
}

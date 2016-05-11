using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KartController : MonoBehaviour {
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

	// general medium stuff
	public float trackIndex;
	public float mediumIndex;

	// Handling traveling in medium
	public int wavelength;
	private bool inMedium;
	public float maxMediumAccelerationReduction;
	public float maxMediumSpeed;
	public float maxMediumSpeedReduction;

	// Handle traveling field
	private bool inField;
	public float maxVacuumSpeed;

	// Handling traveling in the track
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

	//Set buttons on the controller
	public string xaxis;
	public string yaxis;
	public string brakeaxis;
	public string fireButton;

	//Audio for powerups
	private AudioSource powerupSource;
	public AudioClip shieldSound;
	public AudioClip boostSound;
	public AudioClip laserSound;
	public AudioClip blackholeSound;
	public AudioClip pickupSound;

	//Animated text for entering medium
	private Color transparent;
	private Color opaque;
	public MovieTexture atomAnimation;
	public MovieTexture coldAnimation;
	private MovieTexture animationTexture;
	public Sprite leftArrow;
	public Sprite rightArrow;
	private Image arrowImage;

	//Powerup UI icons
	public Sprite shieldIcon;
	public Sprite attackIcon;
	public Sprite blackholeIcon;
	public Sprite boostIcon;
	private Sprite powerupSprite;

	//Auxiliary for Boost FX
	public GameObject myCamera;
	private bool boostActivated;

	//Canvas corresponding to this kart
	public Canvas myCanvas;

	//This kart
	private Rigidbody rb;
	public WaveTailController waveTailController;

	//Initialize
	void Start() {
		rb = GetComponent<Rigidbody> ();
		inElectronOrbit = false;
		drivingAllowed = false;
		shieldsUp = false;
		inMedium = false;
		inField = false;
		attackStartTime = 0f;
		boostActivated = false;
		opaque = new Color(255,255,255,255);
		transparent = new Color(255,255,255,0);
		powerupSource = this.transform.Find ("powerupEffects").GetComponent<AudioSource>();
	}

	//Given a wavelength, returns a Color object.
	//Used for setting color of kart + tail + shield.
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

	//Set color of the shield to the color of the kart
	public void updateWavelength(int color) {
		wavelength = color;
		Renderer shieldRend = shieldObjects[0].GetComponent<Renderer>();
		shieldRend.material.SetColor("_EmissionColor", wavelengthToColor(color));
	}

	//Used to allow driving after race countdown
	public void startDriving() {
		drivingAllowed = true;
	}

	//Start boost related powerups, particles and effects
	private void ActivateBoost() {
		boostActivated = true;
		boostStartTime = Time.time;
		//Loop through all particle emitters in the effect
		Transform speedupFX = myCamera.transform.Find ("SPEEDUPFX");
		ParticleSystem.EmissionModule prt = speedupFX.GetComponent<ParticleSystem> ().emission;
		prt.enabled = true;
		foreach (Transform warp in speedupFX) {
			prt = warp.GetComponent<ParticleSystem> ().emission;
			prt.enabled = true;
		}
		powerupSource.PlayOneShot (boostSound);
	}

	//Kill the Boost effects when boosting is finished
	private void killBoostFX() {
		if (boostActivated) {
			boostActivated = false;
			//Loop through all particle emitters in the effect
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
	}

	//Instantiate a blackhole object
	private void DropBlackHole() {
		Instantiate(blackHolePrefab, transform.position - (10.0f * transform.forward),
			Quaternion.identity);
		powerupSource.PlayOneShot (blackholeSound);
	}

	//Instantiate a lightgun
	private void ShootLightGun() {
		GameObject bullet = (GameObject) Instantiate(lightBallPrefab,
										transform.position + (3.0f * transform.forward),
										Quaternion.identity);
		Homing bulletHoming = (Homing) bullet.GetComponent("Homing");
		bulletHoming.launchKart = this.gameObject;
		powerupSource.PlayOneShot (laserSound);
	}

	//Activate the shield
	private void ShieldsUp() {
		foreach (GameObject shield in shieldObjects) {
			shield.SetActive (true);
		}
		shieldsUp = true;
		shieldStartTime = Time.time;
		powerupSource.PlayOneShot (shieldSound);
	}

	//Deactivate the shields
	public void ShieldsDown() {
		foreach (GameObject shield in shieldObjects) shield.SetActive(false);
		shieldsUp = false;
	}	

	//We check for input in Update
	void Update() {
		//Launch a powerup, if necessary
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
			deactivatePowerupIcon ();
		}
	}

	//Turn off the powerup UI image. Used when powerup is launched. 
	void deactivatePowerupIcon() {
		myCanvas.transform.Find ("Image").GetComponent<Image> ().sprite = null;
		Color newColor = new Color (0, 0, 0);
		newColor.a = 0;
		myCanvas.transform.Find ("Image").GetComponent<Image> ().color = newColor;
	}

	//Drive the car
	void FixedUpdate() {
		//Once a controller is attached
		if (yaxis != "") {
			if (!inElectronOrbit && !inBlackHoleOrbit && drivingAllowed) {
				//Apply player input to the car
				applyDrivingForces ();

			} else if (inBlackHoleOrbit) {
				orbitBlackHole ();
			}
		}
		//respawn when far below the track
		if (this.transform.position.y <= -40) {
			respawn ();
		}
		//Turn off the shields when it's time
		if (shieldsUp && (Time.time - shieldStartTime > shieldTime))
			ShieldsDown();
	}

	//Applies rotations to spin a kart around a blackhole
	void orbitBlackHole() {
		float orbitPhase = (Time.time - bhOrbitTime) * blackHoleOrbitSpeed +
			bhOrbitInitialPhase;
		//Move the kart around in a circle
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

	//Respawns the kart at the last passed checkpoint
	void respawn() {
		CartPosition posHandler = GetComponent <CartPosition> ();
		this.transform.position = posHandler.getRespawnPosition ();
		this.transform.rotation = posHandler.getRespawnRotation ();
		this.GetComponent<Rigidbody> ().velocity = new Vector3(0f,0f,0f);
	}

	//Converts player inputs into forces for kart
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
				if (Input.GetAxis (brakeaxis) != 0.0) {
					lt = (Input.GetAxis (brakeaxis) + 1) / (-2);
				}
				yfloat = (rt + lt);

			} else {
				yfloat = Input.GetAxis (yaxis);
				//Keyboard input
			}
			//When driving on the default track, determine velocity based on wavelength
			if (!inMedium && !inField) {
				float adjustedMaxSpeed = maxTrackSpeed - maxTrackSpeedReduction *
					(1.0f - ((float) (wavelength - 380) / 400.0f));

				//Reduce speed if being affected by lightgun
				if (Time.time < attackStartTime + attackLength)
					adjustedMaxSpeed -= attackSpeedReduction;

				//Accelerate forward
				if (Vector3.Magnitude(rb.velocity) < adjustedMaxSpeed) {
					float adjustedAcceleration = acceleration - maxTrackAccelerationReduction *
						((float) (wavelength - 380) / 400.0f);
					Vector3 forwardForce = transform.forward * adjustedAcceleration * yfloat;
					//Correct force for deltatime and vehicle mass
					forwardForce = forwardForce * Time.deltaTime * rb.mass;
					rb.AddForce(forwardForce);
				}
				//Add a forward force when using a boost powerup
				if (Time.time - boostStartTime < boostTime) {
					rb.AddForce (transform.forward * boostStrength * Time.deltaTime * rb.mass);
				} else {
					killBoostFX ();
				}
				//Reduce speeds when in a medium
			} else if (inMedium) {
				//Determine speed reduction based on wavelength
				float adjustedMaxSpeed = maxMediumSpeed - maxMediumSpeedReduction *
					(1.0f - ((float) (wavelength - 380) / 400.0f));
				//Same for acceleration
				if (Vector3.Magnitude(rb.velocity) < adjustedMaxSpeed) {
					float adjustedAcceleration = acceleration - maxMediumAccelerationReduction *
						(1.0f - ((float) (wavelength - 380) / 400.0f));
					Vector3 forwardForce = transform.forward * adjustedAcceleration * yfloat;
					//Correct force for deltatime and vehicle mass
					forwardForce = forwardForce * Time.deltaTime * rb.mass;
					rb.AddForce(forwardForce);
				}
				//When in the vacuum, lightspeed!
			} else {
				if (Vector3.Magnitude(rb.velocity) < maxVacuumSpeed) {
					Vector3 forwardForce = transform.forward * acceleration * yfloat;
					//Correct force for deltatime and vehicle mass
					forwardForce = forwardForce * Time.deltaTime * rb.mass;
					rb.AddForce(forwardForce);
				}
			}
		//Reduce drag in the air to be more arcade-y
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

	//Set velocity to zero and turn off the RigidBody for a quick sec
	public void EnterAtomOrbit() {
		inElectronOrbit = true;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.Sleep();
	}

	//Get out of the orbit
	public void LeaveAtomOrbit() {
		inElectronOrbit = false;
	}

	//Disable RigidBody velocities when in a blackhole. Transformations done by orbitBlackhole
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

	//Draws the UI icon for powerups
	private void drawPowerupIndicator(Powerups pwr) {
		//Select the right icon
		if (pwr == Powerups.attack) {
			powerupSprite = attackIcon;
		} else if (pwr == Powerups.blackhole) {
			powerupSprite = blackholeIcon;
		} else if (pwr == Powerups.boost) {
			powerupSprite = boostIcon;
		} else {
			powerupSprite = shieldIcon;
		}
		//Insert the image and make sure icon isn't fully transparent
		Image img = myCanvas.transform.FindChild("Image").GetComponent<Image>();
		img.sprite = powerupSprite;
		img.color = new Color (255, 255, 255);
	}

	//Blinks an arrow on a player's canvas
	void BlinkArrow() {
		if (arrowImage.color == transparent) {
			arrowImage.color = opaque;
		} else {
			arrowImage.color = transparent;
		}
	}

	//Begin the text animation when a player enters the atom field
	void startAtomAnim() {
		//Make sure the area isn't transparent
		RawImage rimg = myCanvas.transform.FindChild("Animation").GetComponent<RawImage>();
		Color notTransparant = new Color (255, 255, 255);
		notTransparant.a = 255;
		rimg.color = notTransparant;
		animationTexture = atomAnimation;
		rimg.texture = animationTexture;
		animationTexture.Play ();
	}

	//Begin text animation when player enters cold chamber
	void startColdAnim() {
		RawImage rimg = myCanvas.transform.FindChild("Animation").GetComponent<RawImage>();
		Color notTransparant = new Color (255, 255, 255);
		notTransparant.a = 255;
		rimg.color = notTransparant;
		animationTexture = coldAnimation;
		rimg.texture = animationTexture;
		animationTexture.Play ();
	}
		
	public void OnTriggerEnter(Collider other) {
		//Start the right animations when entering a zone
		if (other.name == "ColdTrigger"){
			startColdAnim();
		}
		if (other.name == "AtomTrigger"){
			startAtomAnim();
		}
		//Put an arrow on the player's screen
		if (other.name == "ArrowTrigger") {
			arrowImage = myCanvas.transform.Find("Arrow").GetComponent<Image>();
			//Teal, green, purple and blue go to atom field
			if (wavelength == 490 || wavelength == 450 || wavelength == 400 || wavelength == 530) {
				arrowImage.sprite = rightArrow;
			}//Everyone else goes to cold zone
			else {
				arrowImage.sprite = leftArrow;
			}
			arrowImage.color = opaque;
			InvokeRepeating ("BlinkArrow", 0f, .5f);
		}
		//Give the player a powerup when colliding with a powerup box
		if (other.name == "Item Box") {
			if (powerup == Powerups.none) {
				float success = 1.0f - ((float) (position - 1) / (float) (totalRacers - 1));
				// Create weights out of 1.0 for each powerup
				//Don't give boost to first place.
				//Don't give shield to last place
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
				//Draw the UI icon for the powerup you were given
				drawPowerupIndicator (powerup);
				powerupSource.PlayOneShot (pickupSound);
			}
			//If you're in a medium, change the refractive index of the tail
		} else if (other.name == "Medium") {
			inMedium = true;
			waveTailController.SetRefractiveIndex(mediumIndex);
			//If you're in a vacuum, also change the refractive index
		} else if (other.name == "Field") {
			inField = true;
			waveTailController.SetRefractiveIndex(1.0f);
		}
	}

	//Remove the black bars and text when leaving medium/atom field
	void stopMovieAnim() {
		animationTexture.Stop ();
		animationTexture = null;
		RawImage rimg = myCanvas.transform.FindChild("Animation").GetComponent<RawImage>();
		rimg.color = transparent;
	}

	//Reset values when leaving triggers/zones
	void OnTriggerExit (Collider other) {
		if (other.name == "Medium") {
			inMedium = false;
			waveTailController.SetRefractiveIndex(trackIndex);
		}
		if (other.name == "Field") {
			inField = false;
			waveTailController.SetRefractiveIndex(trackIndex);
		}
		if (other.name == "ColdTrigger" || other.name == "AtomTrigger") {
			stopMovieAnim ();
		}
		if (other.name == "ArrowTrigger") {
			CancelInvoke ("BlinkArrow");
			arrowImage.color = transparent;
		}
	}

	//Reduce speeds when colliding with a lightgun
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

using UnityEngine;
using System.Collections;

public class Homing : MonoBehaviour {

	//Which kart is launching the lightgun?
	public GameObject launchKart;
	//Information about the target
	private Transform target;
	public CarController targetCtrl;
	//How long effects last
	public float attackTime;
	//How long the shot stays active
	public float timeToLive;
	private float attackStartTime;
	private float instantiateTime;
	private bool reachedTarget;

	//Self-explanatory
	public float boltVelocity;
	public float turningSpeed;
	//The bolt's rigidbody. For applying forces.
	private Rigidbody bolt;

	//Used for looping through possible targets
	private Transform allKarts;

	// Initialization
	void Start () {
		instantiateTime = Time.time;
		boltVelocity = 75f;
		turningSpeed = 20f;
		attackStartTime = -1f;
		reachedTarget = false;
		allKarts = GameObject.Find ("allKarts").transform;

		bolt = GetComponent<Rigidbody>();
		float minDist = Mathf.Infinity;

		//Loop through karts to target the closest kart ahead of you
		bool targetFound = false;
		CarController launchCtrl = launchKart.GetComponent<CarController> ();
		foreach (Transform kart in allKarts) {
			CarController ctrl = kart.GetComponent<CarController> ();
			//Distance between cart and target
			float diff = (kart.position - transform.position).sqrMagnitude;
			//Replace minDist if the min distance so far. But only if target isn't yourself
			if(diff < minDist && kart.GetInstanceID() != launchKart.GetInstanceID()) {
				//Target only if further ahead in race
				if ((kart.tag != launchKart.tag) && (ctrl.position < launchCtrl.position)) {
					targetFound = true;
					minDist = diff;
					target = kart;
					targetCtrl = ctrl;
				}
			}
		}
		//Destroy the shot if no target is found. Prevents null errors
		if (!targetFound)
			Destroy(this.gameObject);
	}
		
	void FixedUpdate () {
		//Destroy if living for too long
		if (Time.time > instantiateTime + timeToLive)
			Destroy(this.gameObject);
		//Set forward velocity
		bolt.velocity = transform.forward * boltVelocity;
		//Rotate to head toward target
		Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
		bolt.MoveRotation (Quaternion.RotateTowards (transform.rotation, targetRotation, turningSpeed));

		float time = Time.time;
		//Make sure the shot doesnt affect target for too long
		if (attackStartTime > 0 && (Time.time > attackStartTime + attackTime))
			Destroy(this.gameObject);
	}

	//Make sure the lightgun doesnt lose its target once it hit
	void OnCollisionEnter(Collision collision) {
		if ((collision.gameObject.tag == target.tag) && (attackStartTime < 0)) {
			if (!targetCtrl.shieldsUp) {
				attackStartTime = Time.time;
				reachedTarget = true;
				boltVelocity = 80f;
			} else {
				Destroy(this.gameObject);
			}
		}
	}
}

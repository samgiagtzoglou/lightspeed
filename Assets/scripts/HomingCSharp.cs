using UnityEngine;
using System.Collections;

public class HomingCSharp : MonoBehaviour {

	public GameObject launcher;
	public GameObject launchCart;
	public CarController targetCtrl;
	public float attackTime;
	public float TTL;
	private float boltVelocity;
	private float turningSpeed;
	private Rigidbody bolt;
	private Transform target;
	private float totalRot;
	private float curRot;
	private Transform allCarts;
	private float attackStartTime;
	private float instantiateTime;
	private bool reachedTarget;

	// Use this for initialization
	void Start () {
		instantiateTime = Time.time;
		boltVelocity = 55f;
		turningSpeed = 20f;
		attackStartTime = -1f;
		reachedTarget = false;
		allCarts = GameObject.Find ("allCarts").transform;

		// AudioSource.PlayClipAtPoint(missileClip, transform.position);
		bolt = GetComponent<Rigidbody>();
		float distance = Mathf.Infinity;
		
		bool targetFound = false;
		CarController launchCtrl = launchCart.GetComponent<CarController> ();
		foreach (Transform cart in allCarts) {
			CarController ctrl = cart.GetComponent<CarController> ();
			float diff = (cart.position - transform.position).sqrMagnitude;
			if(diff < distance && cart.GetInstanceID() != launcher.GetInstanceID()) {
				if ((cart.tag != launchCart.tag) && (ctrl.position < launchCtrl.position)) {
					targetFound = true;
					distance = diff;
					target = cart;
					targetCtrl = ctrl;
				}
			}
		}

		if (!targetFound)
			Destroy(this.gameObject);
	}

	// Update is called once per frame
	void FixedUpdate () {
		// if (reachedTarget && (Vector3.Magnitude(transform.position - target.position) > 5f)) {
		// 	transform.position = target.position +
		// 		((5f / Vector3.Magnitude(transform.position - target.position))
		// 		 * (transform.position - target.position));
		// }

		if (Time.time > instantiateTime + TTL)
			Destroy(this.gameObject);
		bolt.velocity = transform.forward * boltVelocity;
		Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
		bolt.MoveRotation (Quaternion.RotateTowards (transform.rotation, targetRotation, turningSpeed));

		float time = Time.time;
		if (attackStartTime > 0 && (Time.time > attackStartTime + attackTime))
			Destroy(this.gameObject);
	}

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

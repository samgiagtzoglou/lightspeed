using UnityEngine;
using System.Collections;

public class HomingCSharp : MonoBehaviour {

	private float boltVelocity;
	private float turningSpeed;
	private Rigidbody bolt;
	private Transform target;
	public GameObject launcher;
	private float totalRot;
	private float curRot;
	private Transform allCarts;
	public GameObject launchCart;
	public CarController targetCtrl;

	// Use this for initialization
	void Start () {
		boltVelocity = 55f;
		turningSpeed = 20f;
		allCarts = GameObject.Find ("allCarts").transform;
		// AudioSource.PlayClipAtPoint(missileClip, transform.position);
		bolt = GetComponent<Rigidbody>();
		float distance = Mathf.Infinity;
		CarController launchCtrl = launchCart.GetComponent<CarController> ();
		foreach (Transform cart in allCarts) {
//			Debug.Log (cart.tag);
			CarController ctrl = cart.GetComponent<CarController> ();
			float diff = (cart.position - transform.position).sqrMagnitude;
			if(diff < distance && cart.GetInstanceID() != launcher.GetInstanceID()) {
				if ((cart.tag != launchCart.tag) && (ctrl.position < launchCtrl.position)) {
					distance = diff;
					target = cart;
					targetCtrl = ctrl;
				}
			}
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (target == null || bolt == null) {
			Debug.Log ("null path");
			return;
		}
		bolt.velocity = transform.forward * boltVelocity;
		Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
		bolt.MoveRotation (Quaternion.RotateTowards (transform.rotation, targetRotation, turningSpeed));
		//Destroy(this);
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("entered trigger");
		if (other.tag == target.tag) {
			Debug.Log ("hit!");
		}
	}
}

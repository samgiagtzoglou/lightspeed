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

	// Use this for initialization
	void Start () {
		boltVelocity = 45f;
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
					Debug.Log ("got through second if: " + cart.tag);
					distance = diff;
					target = cart;
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
}

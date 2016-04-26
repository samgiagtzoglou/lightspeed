using UnityEngine;
using System.Collections;

public class HomingCSharp : MonoBehaviour {

	public float boltVelocity;
	public float turningSpeed;
	private Rigidbody bolt;
	private Transform target;
	public GameObject launcher;
	private float totalRot;
	private float curRot;
	private Transform allCarts;
	public GameObject launchCart;

	// Use this for initialization
	void Start () {
		allCarts = GameObject.Find ("allCarts").transform;
		// AudioSource.PlayClipAtPoint(missileClip, transform.position);
		bolt = GetComponent<Rigidbody>();
		float distance = Mathf.Infinity;

		foreach (Transform cart in allCarts) {
			CarController ctrl = cart.GetComponent<CarController> ();

			float diff = (cart.position - transform.position).sqrMagnitude;

			if(diff < distance && cart.GetInstanceID() != launcher.GetInstanceID()) {
				if ((transform.name != launchCart.name) && (ctrl.position > launchCart.GetComponent<CarController>().position)) {
					distance = diff;
					target = cart;
				}
			}
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		
		if (target == null || bolt == null) {
			Debug.Log ("target: " + target);
			Debug.Log ("bolt: " + bolt);
			Debug.Log ("exiting");
			return;
		}
		bolt.velocity = transform.forward * boltVelocity;
		Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
		bolt.MoveRotation (Quaternion.RotateTowards (transform.rotation, targetRotation, turningSpeed));
		//Destroy(this);
	}
}

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

	// Use this for initialization
	void Start () {
		// AudioSource.PlayClipAtPoint(missileClip, transform.position);
		bolt = GetComponent<Rigidbody>();
		float distance = Mathf.Infinity;

		foreach (GameObject go in GameObject.FindGameObjectsWithTag("target")) {
			float diff = (go.transform.position - transform.position).sqrMagnitude;

			if(diff < distance && go != launcher) {
				distance = diff;
				target = go.transform;
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

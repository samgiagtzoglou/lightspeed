using UnityEngine;
using System.Collections;

public class PushBall : MonoBehaviour {

	private Rigidbody missile;
	public float thrust;

	// Use this for initialization
	void Start () {
		missile = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		missile.AddForce (thrust * transform.forward);
	}
}

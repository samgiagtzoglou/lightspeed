using UnityEngine;
using System.Collections;

public class SimpleMovement : MonoBehaviour {
	public float speed;
	public Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}

	public void setSpeed(float newSpeed) {
		speed = newSpeed;
	}
	// Update is called once per frame
	void Update () {
		//transform.Translate (speed * Input.GetAxis ("Horizontal") * Time.deltaTime, 0f, speed * Input.GetAxis ("Vertical") * Time.deltaTime);
	}

	void FixedUpdate() {
		rb.AddForce (speed * Input.GetAxis ("Horizontal"), 0f, speed * Input.GetAxis ("Vertical"));
	}
}

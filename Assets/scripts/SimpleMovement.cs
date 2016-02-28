using UnityEngine;
using System.Collections;

public class SimpleMovement : MonoBehaviour {
	public float speed;
	public Rigidbody rb;
	public float rotateSpeed;

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
		transform.Rotate(0f, 0f, rotateSpeed * -Input.GetAxis("Horizontal"));
	}

	void FixedUpdate() {
		Debug.Log (transform.eulerAngles.y);
		rb.AddForce (rb.velocity.z * (1-(transform.eulerAngles.y/180f)), 0f, speed * Input.GetAxis ("Vertical"));
	}
}

using UnityEngine;
using System.Collections;

public class CollectPowerUp : MonoBehaviour {
	
	void OnTriggerEnter(Collider other) {
		//Debug.Log (this.gameObject.GetComponent<SimpleMovement>().speed);
		this.GetComponent<SimpleMovement>().speed = 20;
		this.GetComponent<Rigidbody>().AddForce (0, 10f, 0, ForceMode.Impulse);
	}
}

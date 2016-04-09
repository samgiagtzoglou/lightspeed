using UnityEngine;
using System.Collections;

public class BlackHoleController : MonoBehaviour {

	public float emissionStrength;

	void Start() {
	}

	void OnTriggerEnter(Collider other) {
		StartCoroutine(WaitAndEmit(other));
	}

	IEnumerator WaitAndEmit(Collider other) {
		CarController otherController = (CarController) other.gameObject.GetComponent(typeof(CarController));
		otherController.EnterBlackHoleOrbit(transform.position);
		yield return new WaitForSeconds(5.0f);
		otherController.LeaveBlackHoleOrbit();
		Rigidbody otherRB = other.attachedRigidbody;
		otherRB.AddForce(other.transform.forward * emissionStrength);
	}
}

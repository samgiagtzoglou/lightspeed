using UnityEngine;
using System.Collections;

public class BlackHoleController : MonoBehaviour {

	public float emissionStrength;
	public float numOrbits;

	void Start() {
	}

	void OnTriggerEnter(Collider other) {
		StartCoroutine(WaitAndEmit(other));
	}

	IEnumerator WaitAndEmit(Collider other) {
		CarController otherController = (CarController) other.gameObject.GetComponent(typeof(CarController));
		if (!otherController.shieldsUp) {
			otherController.EnterBlackHoleOrbit(transform.position);
			yield return new WaitForSeconds(numOrbits * 2.0f * Mathf.PI / otherController.blackHoleOrbitSpeed);
			otherController.LeaveBlackHoleOrbit();
			Rigidbody otherRB = other.attachedRigidbody;
			otherRB.AddForce(other.transform.forward * emissionStrength);
		} else {
			otherController.shieldsUp = false;
		}

		Destroy(gameObject);
	}
}

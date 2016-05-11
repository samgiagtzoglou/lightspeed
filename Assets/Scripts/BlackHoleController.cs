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

	//Tell the kart that it's entered a blackhole and start the orbit
	IEnumerator WaitAndEmit(Collider other) {
		KartController otherController = (KartController) other.gameObject.GetComponent(typeof(KartController));
		if (!otherController.shieldsUp) {
			otherController.EnterBlackHoleOrbit(transform.position);
			//Orbit until kart has completed numOrbits
			yield return new WaitForSeconds(numOrbits * 2.0f * Mathf.PI / otherController.blackHoleOrbitSpeed);
			otherController.LeaveBlackHoleOrbit();
			Rigidbody otherRB = other.attachedRigidbody;
			otherRB.AddForce(other.transform.forward * emissionStrength);
		} else {
			otherController.ShieldsDown();
		}

		Destroy(gameObject);
	}
}

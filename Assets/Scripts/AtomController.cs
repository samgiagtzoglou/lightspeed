using UnityEngine;
using System.Collections;

public class AtomController : MonoBehaviour {

	public Vector3 emissionLine;
	public float emissionRange, emissionStrength;
	public GameObject electron;

	private Vector3 emissionVector;
	private ElectronController electronController;

	void Start() {
		electronController = (ElectronController) electron.GetComponent(typeof(ElectronController));
	}

	void OnTriggerEnter(Collider other) {
		StartCoroutine(WaitAndEmit(other));
	}

	IEnumerator WaitAndEmit(Collider other) {
		electronController.AbsorbPhoton();
		CarController otherController = (CarController) other.gameObject.GetComponent(typeof(CarController));
		otherController.EnterOrbit();
		yield return new WaitForSeconds(0.0f);
		electronController.EmitPhoton();
		otherController.LeaveOrbit();
		Rigidbody otherRB = other.attachedRigidbody;
		float emissionAngle = Random.Range(-emissionRange, emissionRange);
		Vector3 emissionForce = Vector3.RotateTowards(emissionLine,
													  Vector3.right,
													  emissionAngle,
													  0.0f) * emissionStrength * otherRB.mass;
		other.transform.rotation = Quaternion.LookRotation(emissionForce);
		otherRB.AddForce(emissionForce);
	}
}

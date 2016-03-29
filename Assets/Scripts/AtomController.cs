using UnityEngine;
using System.Collections;

public class AtomController : MonoBehaviour {

	public Vector3 emissionLine;

	public float emissionRange;

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
		yield return new WaitForSeconds(1.0f);
		float emissionAngle = Random.Range(-emissionRange, emissionRange);
		other.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(emissionLine,
																				 Vector3.right,
																				 emissionAngle,
																				 0.0f));
		electronController.EmitPhoton();
		otherController.LeaveOrbit();
	}
}

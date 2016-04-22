using UnityEngine;
using System.Collections;

public class ItemBoxController : MonoBehaviour {

	public float regenSeconds;

	private Renderer rend;

	void Start() {
		rend = GetComponent<Renderer>();
	}

	void OnTriggerEnter(Collider other) {
		StartCoroutine(TempDisp());
	}

	IEnumerator TempDisp() {
		rend.enabled = false;
		yield return new WaitForSeconds(regenSeconds);
		rend.enabled = true;
	}
}

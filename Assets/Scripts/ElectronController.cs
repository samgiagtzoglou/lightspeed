using UnityEngine;
using System.Collections;

public class ElectronController : MonoBehaviour {

	private Renderer rend;

	void Start() {
		rend = GetComponent<Renderer>();
		rend.material.color = Color.white;
	}

	public void AbsorbPhoton() {
		rend.material.color = Color.cyan;
		transform.localScale *= 1.3f;
	}

	public void EmitPhoton() {
		rend.material.color = Color.white;
		transform.localScale *= 0.7692f;
	}
}

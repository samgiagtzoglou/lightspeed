using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShieldController : MonoBehaviour {

	private Renderer rend;

	void Start() {
		rend = GetComponent<Renderer>();
		rend.enabled = false;
	}

	public void Enable() {
		rend.enabled = true;
	}

	public void Disable () {
		rend.enabled = false;
	}
}

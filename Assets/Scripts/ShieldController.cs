using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShieldController : MonoBehaviour {

	private Renderer rend;

	void Start() {
		rend = this.GetComponent<Renderer> ();
		Debug.Log (rend);
		rend.enabled = false;
		Debug.Log(this.transform.position);
	}

	public void Enable() {
		rend.enabled = true;
	}

	public void Disable () {
		rend.enabled = false;
	}
}

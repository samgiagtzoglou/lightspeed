using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShieldController : MonoBehaviour {

	private Renderer rend;

	void Start() {
		Debug.Log ("hitting init");
		rend = this.GetComponent<Renderer> ();
		Debug.Log (rend);
		rend.enabled = false;
		Debug.Log(this.transform.position);
	}

	public void Enable() {
		Debug.Log ("hitting enable");
		rend.enabled = true;
	}

	public void Disable () {
		rend.enabled = false;
	}
}

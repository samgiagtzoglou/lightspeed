using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShieldController : MonoBehaviour {

	private Animation anime;

	void Start() {
		anime= this.GetComponent<Animation> ();
		Debug.Log (anime);
		anime.enabled = false;
		Debug.Log(this.transform.position);
	}

	public void Enable() {
		anime.enabled = true;
		anime.Play();
	}

	public void Disable () {
		anime.enabled = false;
	}
}

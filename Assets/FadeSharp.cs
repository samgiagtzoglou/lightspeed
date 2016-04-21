using UnityEngine;
using System.Collections;

public class FadeSharp : MonoBehaviour {




	// Use this for initialization
	void Start () {
		StartCoroutine (DoFade ());
	}


	IEnumerator DoFade () {
		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
		while (canvasGroup.alpha <= 0) {
			canvasGroup.alpha += Time.deltaTime / 2;
			yield return null;
		}

		canvasGroup.interactable = false;
		yield return null;

	}
}
using UnityEngine;
using System.Collections;

public class TriggerObject : MonoBehaviour {




	// Use this for initialization
	void Start () {
		//.SetActive (false);
		
	}


	void OnTriggerEnter(Collider other)

	{

		if (other.gameObject.name == "Playground Sticky Bubble Explosion") {

			//Destroy (other.gameObject);
			other.gameObject.SetActive(true);
			//StartCoroutine ("PowerupTimer");
			//other.gameObject.SetActive(true);
		}
	}
}

using UnityEngine;
using System.Collections;

public class SetActiveOnTriggerEnter : MonoBehaviour
{

	void OnTriggerEnter(Collider other)
	{


			
				transform.GetChild(0).transform.gameObject.SetActive(true);
			

	}
}


using UnityEngine;
using System.Collections;

public class WaveRotator : MonoBehaviour {

	void Update () 
	{
		transform.Rotate (new Vector3 (0, 0, 60) * Time.deltaTime);
	}
}

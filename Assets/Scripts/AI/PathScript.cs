using UnityEngine;
using System.Collections;

public class PathScript : MonoBehaviour {

	void OnDrawGizmos() {
		Gizmos.color = Color.white;
		Vector3 pos = transform.position;
		int i = 0;
		Vector3 last = transform.position;
		foreach (Transform child in transform) {
			//Debug.Log (child);
			if (i == 0) {
				pos = child.position;
				last = child.position;
			} else {
				Gizmos.DrawLine (child.position, pos);
				pos = child.position;
			}
			i++;
			Gizmos.DrawWireSphere (child.position, 3);
		}
		Gizmos.DrawLine (last, pos);
	}

}

using UnityEngine;
using System.Collections;

public class RotateToMouse : MonoBehaviour {
	//private Transform trans;
	//private Vector3 pos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.LookAt (new Vector3 (0, 0, Input.mousePosition.x));
	}
}

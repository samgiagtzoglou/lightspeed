using UnityEngine;
using System.Collections;
using System;

public class WaveSegmentController : MonoBehaviour {

    private float groupPhase;

    private Renderer rend;
    
    void Start () {
        rend = GetComponent<Renderer> ();
		groupPhase = transform.position.z * 0.25f;
		rend.material.SetVector ("_Direction", transform.up);
        rend.material.SetFloat ("_TransitionPhase", 0.0f);
		rend.material.SetFloat ("_GroupPhase", groupPhase);
    }

    void Update() {
		Vector4 newDisplacement = new Vector4 (transform.position.x, transform.position.z,
											   0f, 0f);
		rend.material.SetVector ("_Displacement", newDisplacement);
		rend.material.SetVector ("_Direction", transform.up);
    }
}

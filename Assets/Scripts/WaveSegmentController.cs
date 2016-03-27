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
		if (groupPhase > -3.25) {
			rend.material.SetFloat ("_FrontAmplitude", 0.0f);
			rend.material.SetFloat ("_BackAmplitude", 0.1f);
		} else if (groupPhase > -3.28) {
			rend.material.SetFloat ("_FrontAmplitude", 0.1f);
			rend.material.SetFloat ("_BackAmplitude", 0.2f);
		} else if (groupPhase > -3.33) {
			rend.material.SetFloat ("_FrontAmplitude", 0.2f);
			rend.material.SetFloat ("_BackAmplitude", 0.3f);
		} else if (groupPhase > -3.38) {
			rend.material.SetFloat ("_FrontAmplitude", 0.3f);
			rend.material.SetFloat ("_BackAmplitude", 0.4f);
		} else if (groupPhase > -3.43) {
			rend.material.SetFloat ("_FrontAmplitude", 0.4f);
		}
    }

    void Update() {
		Vector4 newDisplacement = new Vector4 (transform.position.x, transform.position.y,
											   transform.position.z, 0f);
		rend.material.SetVector ("_Displacement", newDisplacement);
		rend.material.SetVector ("_Direction", transform.up);
    }
}

using UnityEngine;
using System.Collections;
using System;

public class AvatarController : MonoBehaviour {

    private float groupPhase;

    private Renderer rend;
    
    void Start () {
        rend = GetComponent<Renderer> ();
		rend.material.SetVector ("_Direction", transform.forward);
    }

    void Update() {
		Vector4 newDisplacement = new Vector4 (transform.position.x, transform.position.y,
											   transform.position.z, 0f);
		rend.material.SetVector ("_Displacement", newDisplacement);
		rend.material.SetVector ("_Direction", transform.forward);
    }
}

using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class motionblurtrigger: MonoBehaviour{
	
	Camera camera;
	MotionBlurFilter motionblur;
	void Start () 
	{
		motionblur = (MotionBlurFilter) gameObject.GetComponent(Camera<MotionBlurFilter>);
		motionblur = false;

	
	}

void OnTriggerEnter()
{

		motionblur = true;
	
}

void OnTriggerExit ()
{
	motionblur = false;

	}
}


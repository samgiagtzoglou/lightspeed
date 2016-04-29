using UnityEngine;
using System.Collections;

public class Movie : MonoBehaviour {

	public MovieTexture movie;

	public  void OnTriggerEnter(){

		movie.Play();

	}

	public void OnTriggerExit(){
		movie.Stop ();
	}
}  
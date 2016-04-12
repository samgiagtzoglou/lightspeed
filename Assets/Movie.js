var movieTexture : MovieTexture;
 
  function Start() {
  movieTexture.loop = true;
  GetComponent.<Renderer>().material.mainTexture = movieTexture;
  movieTexture.Play ();
  }
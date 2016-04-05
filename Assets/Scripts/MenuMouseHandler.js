#pragma strict

function OnTriggerEnter2D()
{	
	Debug.Log("test");
    // if we clicked the play button
    if (this.name == "Play")
    {
        // load the game
        Application.LoadLevel("game");
    }
}

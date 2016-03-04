#pragma strict

function OnMouseDown()
{
    // if we clicked the play button
    if (this.name == "Play")
    {
        // load the game
        Application.LoadLevel("game");
    }
}

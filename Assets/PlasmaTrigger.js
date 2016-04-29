#pragma strict

private var guiShow : boolean = false;

var riddle : Texture;
var riddle1 : Texture;
var riddle2 : Texture;

function OnTriggerStay (Col : Collider) {
	guiShow = true;
}

function OnTriggerExit (Col : Collider) {
	guiShow = false;
}

function OnGUI() {
	if(guiShow) {
		//GUI.DrawTexture(Rect(Screen.width - 1140, Screen.height -725, 520, 145), riddle);
				//GUI.DrawTexture(Rect(Screen.width /15500, Screen.height/6000, (Screen.width/.5), Screen.height/10), riddle, ScaleMode.ScaleToFit);
		GUI.DrawTexture(Rect(Screen.width /2, -5, (Screen.width/1.5), Screen.height/15), riddle1, ScaleMode.ScaleToFit);
		GUI.DrawTexture(Rect(0, -5, (Screen.width/1.5), Screen.height/15), riddle2, ScaleMode.ScaleToFit);
		GUI.DrawTexture(Rect(Screen.width /4, -5, (Screen.width/2), Screen.height/15), riddle, ScaleMode.ScaleToFit);
	}
}
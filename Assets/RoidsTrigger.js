#pragma strict

private var guiShow : boolean = false;

var riddle : Texture;
var riddle1 : Texture;
var riddle2 : Texture;

function OnTriggerStay (Col : Collider)

{
	guiShow = true;
	if(Col.tag == "BaseCar")
	{

	}
}

function OnTriggerExit (Col : Collider)
{

guiShow = false;
	if(Col.tag == "BaseCar")
	{
		
	}
}

function OnGUI()
{
	if(guiShow == true)
	{
		//GUI.DrawTexture(Rect(Screen.width - 480, Screen.height -725, 480, 145), riddle);
			//	GUI.DrawTexture(Rect(Screen.width /4, Screen.height /4.5, 480, 145), riddle);
				GUI.DrawTexture(Rect(Screen.width /2, -5, (Screen.width/1.3), Screen.height/14), riddle1, ScaleMode.ScaleToFit);
							  GUI.DrawTexture(Rect(0- Screen.width/8, -5, (Screen.width/1.3), Screen.height/14), riddle2, ScaleMode.ScaleToFit);
			GUI.DrawTexture(Rect(Screen.width /4, 0, (Screen.width/2), Screen.height/16), riddle, ScaleMode.ScaleToFit);

	}
}
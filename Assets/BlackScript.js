﻿#pragma strict

private var guiShow : boolean = false;

var riddle : Texture;

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
		//GUI.DrawTexture(Rect(Screen.width - 1140, Screen.height -725, 520, 145), riddle);
				//GUI.DrawTexture(Rect(Screen.width /15500, Screen.height/6000, (Screen.width/.5), Screen.height/10), riddle, ScaleMode.ScaleToFit);
				GUI.DrawTexture(Rect(Screen.width /4, -5, (Screen.width/2), Screen.height/15), riddle, ScaleMode.ScaleToFit);

	}
}
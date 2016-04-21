#pragma strict

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
		GUI.DrawTexture(Rect(Screen.width - 480, Screen.height -725, 480, 145), riddle);
			//	GUI.DrawTexture(Rect(Screen.width /4, Screen.height /4.5, 480, 145), riddle);

	}
}
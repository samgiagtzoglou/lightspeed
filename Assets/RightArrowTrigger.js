#pragma strict

private var guiShow : boolean = false;
private var blink : boolean = false;

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
      
   if (blink == false){
    //yield WaitForSeconds (5);
      // GUI.DrawTexture(Rect(Screen.width - 480, Screen.height -525, 280, 145), riddle);
       GUI.DrawTexture(Rect(Screen.width /2, Screen.height /2, 280, 145), riddle);
    
      // yield WaitForSeconds (1);
        blink = true;
        print("now true");
        }

        if (blink == true){
       // yield WaitForSeconds (1);
        blink = false;
           print("now false");
        }
            

    }
     
}
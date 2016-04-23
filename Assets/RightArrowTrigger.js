#pragma strict

private var guiShow : boolean = false;
private var blink : boolean = false;

private var blinkNew = false;
 private var counter:int = 11;
 private var blinkSpeed:int = 22;

var riddle : Texture;
var riddle1 : Texture;
var riddle2 : Texture;
var riddle3 : Texture;
var riddle4 : Texture;
var beep : AudioClip;

function OnTriggerStay (Col : Collider)

{
    guiShow = true;



}



function OnTriggerExit (Col : Collider)
{


guiShow = false;
    if(Col.tag == "BaseCar")
    {
        
    }
}


 function Update(){
      if (guiShow){
     if(counter < (blinkSpeed/2)) {
     blinkNew = false;
     counter++;
     } 

    if(counter > (blinkSpeed/2)){
     blinkNew = true;
     counter++;
     }

     if (counter == blinkSpeed/2){
      blinkNew = true;
     counter++;
        GetComponent.<AudioSource>().PlayOneShot(beep);

        }

     if(counter == blinkSpeed){
     blinkNew = true;
     counter = 0;

     }
     }
 }


function OnGUI()
{
    if(guiShow == true)

    {
      
   if (blinkNew){

      // GUI.DrawTexture(Rect(Screen.width - 480, Screen.height -525, 280, 145), riddle);
       //GUI.DrawTexture(Rect(Screen.width /2, 0, 280, 145), riddle);
       //GUI.DrawTexture(Rect(Screen.width/4, 0, (Screen.width/2), Screen.height/5), riddle, ScaleMode.ScaleToFit); FOR NOT BARS
        GUI.DrawTexture(Rect(Screen.width/4, 0, (Screen.width/2), Screen.height/17), riddle, ScaleMode.ScaleToFit);
        GUI.DrawTexture(Rect(Screen.width/2, 0, (Screen.width/2), Screen.height/17), riddle1, ScaleMode.ScaleToFit);
         GUI.DrawTexture(Rect(0, 0, (Screen.width/2), Screen.height/17), riddle2, ScaleMode.ScaleToFit);
         GUI.DrawTexture(Rect(Screen.width/1.4, 0, (Screen.width/2), Screen.height/17), riddle3, ScaleMode.ScaleToFit);
         GUI.DrawTexture(Rect(0-Screen.width/4.6, 0, (Screen.width/2), Screen.height/17), riddle4, ScaleMode.ScaleToFit);

        }

    }
     
}
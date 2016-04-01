 private var blink = false;
 private var counter:int = 0;
 private var blinkSpeed:int = 10;
 public var yourGUITexture:GUITexture;
 
 function Update()
 {
     if(counter == blinkSpeed)
     {
         blink = true;
         counter = 0;
     } 
     else
         blink = false;
     
     counter++;
 }
 
 function OnGUI()
 {
      if(blink)
         yourGUITexture.GetComponent.<GUITexture>().enabled = true;
      else 
         yourGUITexture.GetComponent.<GUITexture>().enabled = false;
 }
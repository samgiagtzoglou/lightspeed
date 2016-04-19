 #pragma strict
 
 private var smallerRect = Rect(20,20,100,50);
 private var biggerRect = Rect(15,15,110,60);
 
 function OnGUI() {
     var rect = smallerRect;
     if (smallerRect.Contains(Event.current.mousePosition))
         rect = biggerRect;
         
     if (GUI.Button(rect, "Some Text")) {
         Debug.Log("Button pressed");
     }
 }

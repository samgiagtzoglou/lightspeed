using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class PositionManager : MonoBehaviour {
	public CartPosition[] allCarts;
	public CartPosition[] carOrder;
	public Dictionary<string,int> cartPositions;

	public void Start() {
		// set up the car objects
		carOrder = new CartPosition[allCarts.Length];
		cartPositions = new Dictionary<string, int>();
		foreach (CartPosition pos in allCarts) {
			cartPositions.Add (pos.name, 1);
		}
		//InvokeRepeating ("ManualUpdate", 0.5f, 0.5f);

	}

	// this gets called every frame
	public void Update() {
		foreach (CartPosition pos in allCarts) {
			int val = pos.GetCarPosition (allCarts);
			cartPositions [pos.name] = val - 1;
			if (pos.name == "kart1") {
				GameObject posTextObj = GameObject.Find ("position");
				Text posText = posTextObj.GetComponent<Text> ();
				posText.text = "Position: " + val;
			}
			//carOrder[pos.GetCarPosition(allCars) - 1] = car;
		}
//		int index = 1;
//		foreach (CartPosition pos in allCarts) {
//			if (pos.name == "kart1") {
//				GameObject posObj = GameObject.Find ("position");
//				Text posText = posObj.GetComponent<Text> ();
//				posText.text = "Position: " + index;
//			}
//			index++;
//		}
		//Debug.Log (carOrder [0]);
	}
}

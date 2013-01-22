using UnityEngine;
using System.Collections;

public class House : MonoBehaviour {
	private bool houseIsShowing = true;
	
	public void ToggleHouse(){
		if (houseIsShowing){
			HideHouse();
		} else {
			
		}
	}
	
	private static void HideHouse(){
		Debug.Log("Hidding");
	}
	
	private static void ShowHouse(){
		Debug.Log("Showing");
	}
}
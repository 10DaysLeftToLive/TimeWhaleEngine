using UnityEngine;
using System.Collections;

public class BuildingContainer : LinkObjectContainer<Building> {
	public void Toggle(bool interiorIsShowing){
		Set(CharacterAgeState.YOUNG, interiorIsShowing);
		Set(CharacterAgeState.MIDDLE, interiorIsShowing);
		Set(CharacterAgeState.OLD, interiorIsShowing);
	}
	
	private void Set(CharacterAgeState age, bool interiorIsShowing){
		if (Get (age) != null) {
			Get(age).ToggleTo(interiorIsShowing);
		}
	}
}

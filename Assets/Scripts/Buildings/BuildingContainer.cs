using UnityEngine;
using System.Collections;

public class BuildingContainer {
	Building youngBuilding;
	Building middleBuilding;
	Building oldBuilding;
	
	public void Toggle(bool interiorIsShowing){
		Set(youngBuilding, interiorIsShowing);
		Set(middleBuilding, interiorIsShowing);
		Set(oldBuilding, interiorIsShowing);
	}
	
	public void SetBuilding(Building newBuilding, CharacterAgeState age){
		switch(age){
			case (CharacterAgeState.YOUNG):
				youngBuilding = newBuilding;
				break;
			case (CharacterAgeState.MIDDLE):
				middleBuilding = newBuilding;
				break;
			case (CharacterAgeState.OLD):
				oldBuilding = newBuilding;
				break;
		}
	}
	
	private void Set(Building toSet, bool interiorIsShowing){
		if (toSet != null){
			toSet.ToggleTo(interiorIsShowing);
		}
	}
}

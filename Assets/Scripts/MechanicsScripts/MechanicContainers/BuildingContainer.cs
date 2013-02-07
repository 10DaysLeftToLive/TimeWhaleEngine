using UnityEngine;
using System.Collections;

public class BuildingContainer : LinkObjectContainer<Building> {
	public void ToggleAll(){
		foreach(CharacterAgeState state in linkedObjects.Keys){
			Get(state).Toggle();
		}
	}
}

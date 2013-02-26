using UnityEngine;
using System.Collections;

public class BuildingContainer : LinkObjectContainer<Building> {
	public override void Perform(){
		ToggleBuildings();
	}
	
	private void ToggleBuildings(){
		foreach(CharacterAgeState state in linkedObjects.Keys){
			Get(state).Toggle();
		}
	}
}

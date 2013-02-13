using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingManager : AbstractContainerManager<Building, BuildingManager, BuildingContainer> {
	public void ToggleWithId(int id){
		if (containersInLevel.ContainsKey(id)){
			((BuildingContainer) containersInLevel[id]).Perform();
		} else {
			Debug.LogWarning("BuildingManager does not contain a building with id " + id);
		}
	}
}
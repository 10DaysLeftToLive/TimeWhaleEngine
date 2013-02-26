using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LockedDoorManager : AbstractContainerManager<LockedDoor, LockedDoorManager, LockedDoorContainer> {
	public void UnlockWithId(int id){
		if (containersInLevel.ContainsKey(id)){
			((LockedDoorContainer) containersInLevel[id]).UnlockAll();
		} else {
			Debug.LogWarning("LockedDoorManager does not contain a door with id " + id);
		}
	}
}
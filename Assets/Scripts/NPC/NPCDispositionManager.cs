using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCDispositionManager : AbstractContainerManager<npcClass, NPCDispositionManager, NPCClassContainer>  {	
	public void UpdateWithId(int id, int newDisposition){
		if (containersInLevel.ContainsKey(id)){
			((NPCClassContainer) containersInLevel[id]).UpdateAll(newDisposition);
		} else {
			Debug.LogWarning("NPCDispositionManager does not contain a building with id " + id);
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCDispositionManager : ManagerSingleton<NPCDispositionManager> {
	static protected Dictionary<int, NPCClassContainer> containersInLevel;
	
	public override void Init(){
		containersInLevel = new Dictionary<int, NPCClassContainer >();
	}
	
	public void Add(NPC objectToAdd, CharacterAgeState ageToAdd){
		containersInLevel[objectToAdd.ID].Add(objectToAdd, ageToAdd);
	}
	
	// Load in all objects that this manager should handle from the given age root
	public void LoadInObjectsToManage(Transform rootOfAge, CharacterAgeState ageRootIn){
		Component[] componentsToManage = (Component[])rootOfAge.GetComponentsInChildren(typeof(NPC));
		
		foreach (NPC objectToManage in componentsToManage){
			if (!containersInLevel.ContainsKey(objectToManage.ID)){
				containersInLevel.Add(objectToManage.ID, new NPCClassContainer());
				objectToManage.ageNPCisIn = ageRootIn;
			}
			Add (objectToManage, ageRootIn);
		}
	}

	public void UpdateWithId(int id, int newDisposition){
		if (containersInLevel.ContainsKey(id)){
			((NPCClassContainer) containersInLevel[id]).UpdateAll(newDisposition);
		} else {
			Debug.LogWarning("NPCDispositionManager does not contain an NPC with id " + id);
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCDispositionManager : ManagerSingleton<NPCDispositionManager> {
	Dictionary<int, NPCClassContainer> npcs;
	
	public override void Init(){
		npcs = new Dictionary<int, NPCClassContainer >();
	}
	
	public void LoadInNPCs(Transform sectionRoot, CharacterAgeState age){
		Component[] components = (Component[])sectionRoot.GetComponentsInChildren(typeof(npcClass));
		
		foreach (npcClass npc in components){
			if (!npcs.ContainsKey(npc.id)){
				npcs.Add(npc.id, new NPCClassContainer());
			}
			npcs[npc.id].Add(npc, age);
		}
	}
	
	public void UpdateWithId(int id, int newDisposition){
		if (npcs.ContainsKey(id)){
			npcs[id].UpdateAll(newDisposition);
		} else {
			Debug.LogWarning("NPCDispositionManager does not contain a building with id " + id);
		}
	}
	
	public void Add(npcClass toAdd, CharacterAgeState age){		
		npcs[toAdd.id].Add(toAdd, age);
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCDispositionManager : MonoBehaviour {
	Dictionary<int, NPCClassContainer> npcs;
	
	void Awake(){
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
	
	public void UpdateWithId(int id, float newDisposition){
		if (npcs.ContainsKey(id)){
			npcs[id].UpdateDisposition(newDisposition);
		} else {
			Debug.LogWarning("NPCDispositionManager does not contain a building with id " + id);
		}
	}
	
	public void Add(npcClass toAdd, CharacterAgeState age){		
		npcs[toAdd.id].Add(toAdd, age);
	}
	
	#region Singleton
	private static NPCDispositionManager npcm_instance = null;
	
	public static NPCDispositionManager instance{
		get {
            if (npcm_instance == null) {
                npcm_instance = FindObjectOfType(typeof (NPCDispositionManager)) as NPCDispositionManager;
            }
 
            // If it is still null, create a new instance
            if (npcm_instance == null) {
                GameObject obj = new GameObject("NPCDispositionManager");
                npcm_instance = obj.AddComponent(typeof (NPCDispositionManager)) as NPCDispositionManager;
            }
 
            return npcm_instance;
        }
	}
	#endregion
}

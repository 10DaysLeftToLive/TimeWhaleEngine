using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class NPCManager : ManagerSingleton<NPCManager> {
	private static Dictionary<string, GameObject> dictNPC = new Dictionary<string, GameObject>();
	
	public void Add(GameObject npc) {
		dictNPC.Add(npc.name, npc);
	}
	
	public NPC getNPC(string npcName) {
		return dictNPC[npcName].GetComponent<NPC>();
	}
	
	public Dictionary<string, GameObject> getNPCDictionary() {
		return dictNPC;
	}
	
	public List<Flag> GetFlags(){
		List<Flag> allNPCFlags = new List<Flag>();
		
		foreach (GameObject npc in dictNPC.Values){
			NPC npcClass = npc.GetComponent<NPC>();
			Debug.Log("Getting flags of " + npc.name);
			List<string> currentNPCFlags = npcClass.GetFlags();
			foreach (string flag in currentNPCFlags){
				Debug.Log("Adding flag " + flag);
				Flag newFlag = new Flag(flag);
				if (!allNPCFlags.Contains(newFlag)){
					allNPCFlags.Add(newFlag);
				}
				int index = allNPCFlags.IndexOf(newFlag);
				allNPCFlags[index].AddNPC(npcClass);
			}
			currentNPCFlags.Clear();
		}
		return(allNPCFlags);
	}
}

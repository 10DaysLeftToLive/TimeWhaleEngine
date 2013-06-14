using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class NPCManager : ManagerSingleton<NPCManager> {
	private static Dictionary<string, NPC> dictNPC = new Dictionary<string, NPC>();

	public void Add(GameObject npc) {
		if (dictNPC.ContainsKey(npc.name)){
			dictNPC.Remove(npc.name);
		}
		dictNPC.Add(npc.name, npc.GetComponent<NPC>());
	}
	
	public void Add(NPC npc) {
		if (dictNPC.ContainsKey(npc.name)){
			dictNPC.Remove(npc.name);
		}
		dictNPC.Add(npc.gameObject.name, npc);
	}
	
	public NPC getNPC(string npcName) {
		if (dictNPC.ContainsKey(npcName)){
			return dictNPC[npcName];
		} else {
			GameObject toFind = GameObject.Find(npcName);
			if (toFind == null){
				Debug.LogError("Could not find the npc " + npcName);
				return (null);
			} else {
				NPC foundNpc = toFind.GetComponent<NPC>();
				Add(foundNpc);
				return (foundNpc);
			}
		}
	}
	
	public Dictionary<string, NPC> getNPCDictionary() {
		return dictNPC;
	}
	
	public void RemoveSchedulesWithFlag(string flag) {
		foreach (NPC npc in dictNPC.Values) {
			npc.RemoveScheduleWithFlag(flag);
		}
	}
	
	public List<Flag> GetFlags(){
		List<Flag> allNPCFlags = new List<Flag>();
		foreach (NPC npc in dictNPC.Values){
			List<string> currentNPCFlags = npc.GetFlags();
			foreach (string flag in currentNPCFlags){
				Flag newFlag = new Flag(flag);
				if (!allNPCFlags.Contains(newFlag)){
					allNPCFlags.Add(newFlag);
				}
				int index = allNPCFlags.IndexOf(newFlag);
				allNPCFlags[index].AddNPC(npc);
			}
			currentNPCFlags.Clear();
		}
		return(allNPCFlags);
	}
}

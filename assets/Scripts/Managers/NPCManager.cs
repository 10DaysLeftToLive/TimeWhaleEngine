using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class NPCManager : ManagerSingleton<NPCManager> {
	private static Dictionary<string, NPC> dictNPC = new Dictionary<string, NPC>();
	
	public void Add(GameObject npc) {
		dictNPC.Add(npc.name, npc.GetComponent<NPC>());
	}
	
	public void Add(NPC npc) {
		dictNPC.Add(npc.gameObject.name, npc);
	}
	
	public NPC getNPC(string npcName) {
		return dictNPC[npcName];
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
			Debug.Log("Getting flags of " + npc.gameObject.name);
			List<string> currentNPCFlags = npc.GetFlags();
			foreach (string flag in currentNPCFlags){
				Debug.Log("Adding flag " + flag);
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

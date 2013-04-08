using UnityEngine;
using System.Collections.Generic;

public class NPCManager : ManagerSingleton<NPCManager> {
	private static Dictionary<string, GameObject> dictNPC = new Dictionary<string, GameObject>();
	
	public void Add(GameObject npc) {
		dictNPC.Add(npc.name, npc);
	}
	
	public GameObject getNPC(string npcName) {
		return dictNPC[npcName];
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class InteractionManager : ManagerSingleton<InteractionManager> {
	private PlayerController playerCharacter;

	public override void Init(){
		playerCharacter = GameObject.FindObjectOfType(typeof(PlayerController)) as PlayerController;		
	}
	
	public void SaveNPCDispositions(string disposiitonData){
		NPCCollection npcCollection = new NPCCollection();
		
		GameObject[] npcs = GetNPCs();
		NPC npc_Class;
		
		foreach (GameObject npc in npcs){
			NPCData dataForNPC = new NPCData();
			npc_Class = npc.GetComponent<NPC>();
			dataForNPC.disposition = npc_Class.GetDisposition();
			dataForNPC.name = npc.name;		
			Debug.Log("Disposition for " + dataForNPC.name + " = " + dataForNPC.disposition);;
			npcCollection.Add(dataForNPC);
		}
		npcCollection.Save(disposiitonData);
	}

	public void InitilizeNPCs(string dispositionData, string levelData){
		NPCCollection npcCollection = NPCCollection.Load(dispositionData);
		NPCToItemCollection npcToItems = NPCToItemCollection.Load(levelData);
		
		GameObject[] npcs = GetNPCs();
		NPC npc_Class;
		NPCData currentData = new NPCData();
		NPCItemsReactions currentNPCReactions = new NPCItemsReactions();
		
		foreach (GameObject npc in npcs){
			currentData = npcCollection.GetNPC(npc.name);
			npc_Class = npc.GetComponent<NPC>();
			
			currentNPCReactions = npcToItems.GetNPC(npc.name);
			
			SetNPCDisposition(npc_Class, currentData);
			SetNPCInteractions(npc_Class, currentNPCReactions);			
		}
	}
	
	private GameObject[] GetNPCs(){
		return (GameObject.FindGameObjectsWithTag(Strings.tag_NPC)); 
	}
	
	private void SetNPCInteractions(NPC npcClass, NPCItemsReactions data){		
		npcClass.SetInteractions(data.items);
	}
	
	private void SetNPCDisposition(NPC npcClass, NPCData data){
		npcClass.SetDisposition(data.disposition);
	}
	
	public void PerformInteraction(GameObject targetNPC){
		if (playerCharacter.HasItem()){
			GameObject playerItem = playerCharacter.GetItem();
			//targetNPC.GetComponent<NPC>().ReactTo(playerItem.name);
			//Give item to NPC
		} else {
			//targetNPC.GetComponent<NPC>().ReactTo(Strings.NoItem);
		}
	}
}
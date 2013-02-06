using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class InteractionManager : MonoBehaviour {
	private PlayerController playerCharacter;
	
	public TextAsset disData;
	public TextAsset interactionData;
	
	
	// Use this for initialization
	void Start () {
		playerCharacter = FindObjectOfType(typeof(PlayerController)) as PlayerController;		
	}
	
	public void SaveNPCDispositions(string disposiitonData){
		NPCCollection npcCollection = new NPCCollection();
		
		GameObject[] npcs = GetNPCs();
		npcClass npc_Class;
		
		foreach (GameObject npc in npcs){
			NPCData dataForNPC = new NPCData();
			npc_Class = npc.GetComponent<npcClass>();
			dataForNPC.disposition = npc_Class.GetDisposition() + 2;
			dataForNPC.name = npc.name;			
			npcCollection.Add(dataForNPC);
		}
		npcCollection.Save(disposiitonData);
	}

	public void InitilizeNPCs(string dispositionData, string levelData){
		NPCCollection npcCollection = NPCCollection.Load(dispositionData);
		NPCToItemCollection npcToItems = NPCToItemCollection.Load(levelData);
		
		GameObject[] npcs = GetNPCs();
		npcClass npc_Class;
		NPCData currentData = new NPCData();
		NPCItemsReactions currentNPCReactions = new NPCItemsReactions();
		
		foreach (GameObject npc in npcs){
			currentData = npcCollection.GetNPC(npc.name);
			npc_Class = npc.GetComponent<npcClass>();
			
			currentNPCReactions = npcToItems.GetNPC(npc.name);
			
			SetNPCDisposition(npc_Class, currentData);
			SetNPCInteractions(npc_Class, currentNPCReactions);			
		}
	}
	
	private GameObject[] GetNPCs(){
		return (GameObject.FindGameObjectsWithTag(Strings.tag_NPC)); 
	}
	
	private void SetNPCInteractions(npcClass npcClass, NPCItemsReactions data){		
		npcClass.SetInteractions(data.items);
	}
	
	private void SetNPCDisposition(npcClass npcClass, NPCData data){
		npcClass.SetDisposition(data.disposition);
	}
	
	public void PerformInteraction(GameObject targetNPC){
		if (playerCharacter.HasItem()){
			GameObject playerItem = playerCharacter.GetItem();
			targetNPC.GetComponent<npcClass>().ReactTo(playerItem.name);
			//Give item to NPC
		} else {
			targetNPC.GetComponent<npcClass>().ReactTo(Strings.NoItem);
		}
	}
	
	private static InteractionManager im_instance = null;
	
	public static InteractionManager instance{
		get {
            if (im_instance == null) {
                //  FindObjectOfType(...) returns the first ScreenSetup object in the scene.
                im_instance =  FindObjectOfType(typeof (InteractionManager)) as InteractionManager;
            }
 
            // If it is still null, create a new instance
            if (im_instance == null) {
                GameObject obj = new GameObject("InteractionManager");
                im_instance = obj.AddComponent(typeof (InteractionManager)) as InteractionManager;
            }
 
            return im_instance;
        }
	}
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

public class InteractionManager : MonoBehaviour {
	public PlayerController playerCharacter;
	private Dictionary<string, Dictionary<string, float>> convertedInteractionToDispositionChange;
	
	// Use this for initialization
	void Start () {
		convertedInteractionToDispositionChange = new Dictionary<string, Dictionary<string, float>>();
		
	}
	
	/// <summary>
	/// Initilizes the NPC disposition dict. Should be called from LevelManager
	/// </summary>
	/// <param name='npcDispositionDict'>
	/// Npc disposition dict. Dictionary<string NPCName, float disposition>
	/// </param>
	public void InitilizeNPCDispositionDict(Dictionary<string, Dictionary<string, float>> npcDispositionDict, string levelData){
		LoadInteractionItemTable(levelData);	
		/*
		foreach(string npcName in npcDispositionDict.Keys){
			SetNPCInteractionItemTable();
			SetNPCDisposition();
		}*/
	}
	
	private void LoadInteractionItemTable(string levelData){
		convertedInteractionToDispositionChange = ReadNPCData.ReadNPCItemsDispositonFromFile(levelData);
	}
	
	private void SetNPCInteractionItemTable(){
		//Should take in an NPC script to call function on
	}
	
	private void SetNPCDisposition(){
		//Should take in an NPC script to call function on
	}
	
	public void PerformInteraction(GameObject targetNPC){
		if (playerCharacter.HasItem()){
			GameObject playerItem = playerCharacter.GetItem();
			FindAndChangeDisposition(targetNPC, playerItem.name);
			//Give item to NPC
		} else {
			FindAndChangeDisposition(targetNPC, Strings.NoItem);
		}
	}
	
	private void FindAndChangeDisposition(GameObject targetNPC, string interactableObject){
		float dispositionChange = 0;
		
		if (FindDispositionChange(targetNPC, interactableObject, out dispositionChange)){
			//Do disposition changing
		}
		MetricsRecorder.RecordInteraction(targetNPC.name, interactableObject, dispositionChange);
	}
	
	private bool FindDispositionChange(GameObject targetNPC, string interactableObject, out float dispositionChange){
		string npcName = targetNPC.name;
		string objectName = interactableObject;
		dispositionChange = 0;
		Dictionary<string, float> itemsToDispostions;
		
		if (ContainsNPC(npcName)){
			itemsToDispostions = convertedInteractionToDispositionChange[npcName];
			
			if (NPCHasItemRelation(itemsToDispostions, objectName)){
				dispositionChange = itemsToDispostions[objectName];
				Debug.Log("Disposition Change = " + dispositionChange);
				return (true);
			} else {
				Debug.Log("No item data was given between " + npcName + " and " + objectName);
			}
		} else {
			Debug.Log("No npc data was given for " + npcName);
		}
		return (false);
	}
	
	private bool ContainsNPC(string name){
		return (convertedInteractionToDispositionChange.ContainsKey(name));
	}
	
	private bool NPCHasItemRelation(Dictionary<string, float> itemsToDispostionsForNPC, string item){
		return (itemsToDispostionsForNPC.ContainsKey(item));
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
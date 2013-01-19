using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionManager : MonoBehaviour {
	public PlayerController playerCharacter;
	private Dictionary<string, float> convertedInteractionToDispositionChange;
	
	// Use this for initialization
	void Start () {
		convertedInteractionToDispositionChange = new Dictionary<string, float>();
	}
	
	/// <summary>
	/// Initilizes the NPC disposition dict. Should be called from LevelManager
	/// </summary>
	/// <param name='npcDispositionDict'>
	/// Npc disposition dict. Dictionary<string NPCName, float disposition>
	/// </param>
	public void InitilizeNPCDispositionDict(Dictionary<string,float> npcDispositionDict){
		LoadInteractionItemTable();	
		
		foreach(string npcName in npcDispositionDict.Keys){
			SetNPCInteractionItemTable();
			SetNPCDisposition();
		}
	}
	
	private void LoadInteractionItemTable(){
		convertedInteractionToDispositionChange = new Dictionary<string, float>();
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
		string key = targetNPC.name + " to " + interactableObject;
		dispositionChange = 0;
		
		if (convertedInteractionToDispositionChange.ContainsKey(key)){
			convertedInteractionToDispositionChange.TryGetValue(key, out dispositionChange);
			return (true);
		} else {
			Debug.Log("There was no interaction set for " + key);
			return (false);
		}
	}
	
	private void TurnAttachedInteractionsIntoDictionary(){
		convertedInteractionToDispositionChange = new Dictionary<string, float>();
		
		Component[] allInteractions;
		allInteractions = (Component[])this.GetComponents(typeof(Interaction));
		foreach (Interaction interaction in allInteractions) {
			if (interaction.targetNPC == null){
				Debug.LogError("Warning - Interaction Target NPC not set");
			} else if (interaction.targetInteractableObject == null){
				Debug.LogError("Warning - Interaction Target Object not set");
			} else {
				string combinedKey = interaction.targetNPC.name + " to " + interaction.targetInteractableObject.name;
				float dispositionChange = interaction.dispositionChange;
				
				convertedInteractionToDispositionChange.Add(combinedKey, dispositionChange);
			}
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
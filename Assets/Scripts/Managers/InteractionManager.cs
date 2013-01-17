using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionManager : MonoBehaviour {
	private Dictionary<string, float> convertedInteractionToDispositionChange;
	
	// Use this for initialization
	void Start () {
		TurnAttachedInteractionsIntoDictionary();
	}
	
	public void PerformInteraction(GameObject targetNPC, GameObject interactableObject){
		float dispositionChange = 0;
		
		if (FindDispositionChange(targetNPC, interactableObject, out dispositionChange)){
			//Do disposition changing
		}		
	}
	
	private bool FindDispositionChange(GameObject targetNPC, GameObject interactableObject, out float dispositionChange){
		string key = targetNPC.name + " to " + interactableObject.name;
		dispositionChange = 0;
		
		if (convertedInteractionToDispositionChange.ContainsKey(key)){
			convertedInteractionToDispositionChange.TryGetValue(key, out dispositionChange);
			return true;
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
}
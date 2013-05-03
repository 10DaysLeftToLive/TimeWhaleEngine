using UnityEngine;
using System.Collections;

/// <summary>
/// Update current text action will update the current text in the interaction display. It will change back to the default
/// when the player leaves.
/// </summary>
public class UpdateCurrentTextAction : InteractionUpdateAction {
	public UpdateCurrentTextAction(NPC npcToUpdate, string newText) : base(npcToUpdate, newText){}
	
	public override void Perform(){
		GUIManager.Instance.UpdateInteractionDisplay(newText);
	}
}

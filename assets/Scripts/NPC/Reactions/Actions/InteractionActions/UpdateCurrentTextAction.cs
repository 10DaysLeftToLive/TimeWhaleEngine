using UnityEngine;
using System.Collections;

public class UpdateCurrentTextAction : InteractionUpdateAction {
	public UpdateCurrentTextAction(NPC npcToUpdate, string newText) : base(npcToUpdate, newText){}
	
	public override void Perform(){
		GUIManager.Instance.UpdateInteractionDisplay(newText);
	}
}

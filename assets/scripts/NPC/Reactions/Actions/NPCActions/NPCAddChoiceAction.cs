using UnityEngine;
using System.Collections;

/// <summary>
/// NPC add choice action will add a given choice and reaction to the current emotion state
/// </summary>
public class NPCAddChoiceAction : NPCValueUpdateAction {
	Choice newChoice;
	DispositionDependentReaction reaction;
	
	public NPCAddChoiceAction(NPC _npcToUpdate, Choice _newChoice, DispositionDependentReaction _reaction) : base(_npcToUpdate) {
		newChoice = _newChoice;
		reaction = _reaction;
	}
	
	public override void Perform(){
		npcToUpdate.AddChoice(newChoice, reaction);
		if (npcToUpdate.IsInteracting()) {
			GUIManager.Instance.RefreshInteraction();
		}
	}
}
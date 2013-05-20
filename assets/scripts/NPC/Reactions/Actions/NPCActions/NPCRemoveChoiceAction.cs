using UnityEngine;
using System.Collections;

/// <summary>
/// NPC remove choice action will remove the choice form the 
/// </summary>
public class NPCRemoveChoiceAction : NPCValueUpdateAction {
	Choice choiceToRemove;
	
	public NPCRemoveChoiceAction(NPC _npcToUpdate, Choice _choiceToRemove) : base(_npcToUpdate) {
		choiceToRemove = _choiceToRemove;
	}
	
	public override void Perform(){
		npcToUpdate.RemoveChoice(choiceToRemove);
		if (npcToUpdate.IsInteracting()) {
			GUIManager.Instance.RefreshInteraction();
		}
	}
}
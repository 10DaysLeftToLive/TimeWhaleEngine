using UnityEngine;
using System.Collections;

/// <summary>
/// NPC change portrait action will change the portrait of the given npc
/// </summary>
public class NPCChangePortraitAction : NPCValueUpdateAction {
	string portait;
	
	public NPCChangePortraitAction(NPC _npcToUpdate, string newPortrait) : base(_npcToUpdate) {
		portait = newPortrait;
	}
	
	public override void Perform(){
		npcToUpdate.SetCharacterPortrait(portait);
		if (npcToUpdate.IsInteracting()){
			GUIManager.Instance.RefreshInteraction();
		}
	}
}
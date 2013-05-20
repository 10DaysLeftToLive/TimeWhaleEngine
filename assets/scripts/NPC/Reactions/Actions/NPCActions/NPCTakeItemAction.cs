using UnityEngine;
using System.Collections;

public class NPCTakeItemAction : NPCValueUpdateAction {
	public NPCTakeItemAction(NPC _npcToUpdate) : base(_npcToUpdate) {
	}
	
	public override void Perform(){
		npcToUpdate.player.DisableHeldItem();
		GUIManager.Instance.RefreshInteraction(); // we need to get ride of the give button
	}
}
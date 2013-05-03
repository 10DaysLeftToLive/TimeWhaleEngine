using UnityEngine;
using System.Collections;

public class NPCTakeItemAction : NPCValueUpdateAction {
	public NPCTakeItemAction(NPC _npcToUpdate) : base(_npcToUpdate) {
	}
	
	public override void Perform(){
		npcToUpdate.player.DisableHeldItem();
	}
}
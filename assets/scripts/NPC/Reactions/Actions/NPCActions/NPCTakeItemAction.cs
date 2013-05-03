using UnityEngine;
using System.Collections;

/// <summary>
/// NPCTakeItemAction will remove the item from the player's inventory
/// </summary>
public class NPCTakeItemAction : NPCValueUpdateAction {
	public NPCTakeItemAction(NPC _npcToUpdate) : base(_npcToUpdate) {
	}
	
	public override void Perform(){
		if (npcToUpdate.player.Inventory.HasItem()){
			npcToUpdate.player.DisableHeldItem();
		} else {
			Debug.LogWarning("Trying to perform a remove action when player did not have an item.");	
		}
	}
}
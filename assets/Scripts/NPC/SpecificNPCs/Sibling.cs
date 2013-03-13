using UnityEngine;
using System.Collections;

public class Sister : NPC {
	protected override void ReactToItemInteraction(string npc, string item){
		Debug.Log(name + " is reacting to " + npc + " getting " + item);
	}
	
	protected override void ReactToChoiceInteraction(string npc, string choice){
		Debug.Log(name + " is reacting to player making choice " + choice + " with " + npc);
	}
	
	protected override string GetWhatToSay(){
		return (this.name + " says hai! I am feeling " + npcDisposition);
	}
	
	protected override void LeftButtonCallback(){
		Debug.Log(this.name + " left callback");
		EventManager.instance.RiseOnNPCInteractionEvent(new NPCItemInteraction(this.gameObject, player.Inventory.GetItem().name));
		// TODO? this is for a chat dialoge
	}
	
	protected override void RightButtonCallback(){
		Debug.Log(this.name + " right callback");
		GameObject item = player.Inventory.GetItem();
		DoReaction(item);
	}
	
	protected override void DoReaction(GameObject itemToReactTo){
		if (itemToReactTo != null){
			Debug.Log(name + " is reacting to: " + itemToReactTo.name);
			switch (itemToReactTo.tag){
				case "Plushie":
					break;
				case "Frisbee":
					break;
				default:
					break;
			}
			player.Inventory.DisableHeldItem();
		}
	}
}

using UnityEngine;
using System.Collections;

public class Mom : NPC {	
	protected override void ReactToItemInteraction(string npc, string item){
		Debug.Log(name + " is reacting to " + npc + " getting " + item);
	}
	
	protected override void ReactToChoiceInteraction(string npc, string choice){
		Debug.Log(name + " is reacting to player making choice " + choice + " with " + npc);
	}
	
	protected override string GetWhatToSay(){
		return ("Can you go get apples for me to make this pie?");
	}
	
	protected override void LeftButtonCallback(){
		Debug.Log(this.name + " left callback");
		// TODO? this is for a chat dialoge
		EventManager.instance.RiseOnNPCInteractionEvent(new NPCChoiceInteraction(this.gameObject, "Default"));
	}
	
	protected override void RightButtonCallback(){
		Debug.Log(this.name + " right callback");
		GameObject item = player.Inventory.GetItem();
		DoReaction(item);
	}
	
	protected override void DoReaction(GameObject itemToReactTo){
		if (itemToReactTo != null){
			Debug.Log(name + " is reacting to: " + itemToReactTo.name);
			switch (itemToReactTo.name){
				case "Plushie":
					UpdateChat("Thanks kid.");
					break;
				case "Apple":
					UpdateChat("Perfect! These will work wonderfully!");
					break;
				default:
					break;
			}
			player.Inventory.DisableHeldItem();
		}
	}
}
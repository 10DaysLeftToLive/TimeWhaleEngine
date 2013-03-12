using UnityEngine;
using System.Collections;

public class Mom : NPC {
	protected override string GetWhatToSay(){
		return (this.name + " says hai! I am feeling " + npcDisposition);
	}
	
	protected override void LeftButtonCallback(){
		Debug.Log(this.name + " left callback");
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
			switch (itemToReactTo.name){
				case "Plushie":
					UpdateDisposition(10);
					UpdateChat("Thanks kid.");
					break;
				default:
					break;
			}
			player.Inventory.DisableHeldItem();
		}
	}
}

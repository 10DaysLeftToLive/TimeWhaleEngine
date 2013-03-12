using UnityEngine;
using System.Collections;

public class Mom : NPC {
	protected override string GetWhatToSay(){
		return ("Can you go get apples for me to make this pie?");
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

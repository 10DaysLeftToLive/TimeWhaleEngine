using UnityEngine;
using System.Collections;

public class Mom : npcClass {
	protected override void DoReaction(string itemToReactTo){
		switch (itemToReactTo){
			case "GoldenGear":
				npcDisposition += 10;
				(player.GetComponent<PlayerController>() as PlayerController).DestroyHeldItem();
				break;
			case "NoItem":
				break;
			default:
				break;
		Debug.Log("Doing reaction between " + name + " and " + itemToReactTo);
		if (itemToReactTo == "GoldenGear") {
			questDone = true;
		}
		if (npcDisposition > 5){
			// blah
		}
	}
}

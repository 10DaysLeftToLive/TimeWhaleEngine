using UnityEngine;
using System.Collections;

public class Mom : npcClass {
	protected override void DoReaction(string itemToReactTo){
		Debug.Log("Doing reaction between " + name + " and " + itemToReactTo);
		if (itemToReactTo == "GoldenGear") {
			questDone = true;
		}
		if (npcDisposition > 5){
			// blah
		}
	}
}

using UnityEngine;
using System.Collections;

public class PaperBoy : npcClass {
	protected override void DoReaction(string itemToReactTo){
		Debug.Log("Doing reaction between " + name + " and " + itemToReactTo);
		
		if (npcDisposition > 5){
			// blah
		}
	}
}

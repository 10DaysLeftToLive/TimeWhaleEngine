using UnityEngine;
using System.Collections;

public class CatLady : npcClass {
	protected override void DoReaction(string itemToReactTo){
		Debug.Log("Doing reaction between " + name + " and " + itemToReactTo);
		
		if (npcDisposition > 5){
			// blah
		}
	}
}

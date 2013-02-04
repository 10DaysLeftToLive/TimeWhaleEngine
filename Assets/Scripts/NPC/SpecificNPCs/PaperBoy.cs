using UnityEngine;
using System.Collections;

public class PaperBoy : npcClass {
	
	public bool inLove;//TODO: Need to make a better getter/setter system.
	
	public GameObject playerSister;
	
	protected override void DoReaction(string itemToReactTo) {
		if (itemToReactTo == "Flower") {
			inLove = true;
		}
		FallInLove();
		
		if (npcDisposition > 5){
			// blah
		}
	}
	
	void FallInLove() {
		if ((playerSister.GetComponent<Sister>() as  Sister).inLove && inLove) {
			Debug.Log ("Paper Boy says: The Paper Boy and the Sister are in Love");
		}
	}
	
}

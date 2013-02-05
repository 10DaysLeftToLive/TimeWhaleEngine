using UnityEngine;
using System.Collections;

public class PaperBoy : npcClass {
	
	public bool inLove;//TODO: Need to make a better getter/setter system.
	
	public GameObject playerSister;
	
	protected override void DoReaction(string itemToReactTo) {
		switch (itemToReactTo){
			case "Flower":
				inLove = true;
				npcDisposition += 10;
				(player.GetComponent<PlayerController>() as PlayerController).DestroyHeldItem();
				break;
			case "NoItem":
				break;
			default:
				break;
		}
		
		
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
